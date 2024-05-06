using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Net;

using Account.Data;
using Account.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Hosting;

public class EmailSender : IEmailSender
{
    private readonly IEmailSettingData rep;
    private readonly IWebHostEnvironment environment;

    public EmailSender(IEmailSettingData rep, IWebHostEnvironment environment)
    {
        this.rep = rep;
        this.environment = environment;
    }

    public List<string> Sendto { get; set; }
    public List<string> SendCC { get; set; }
    public List<string> SendBCC { get; set; }
    public string Subject { get; set; } = "Smart Admin";
    public string ToName { get; set; } = "Customer";
    public string ImageURL { get; set; } = "";
    public string Description { get; set; }
    public string ActionName { get; set; }
    public string URL { get; set; }

    public string Attachment { get; set; } = "";

    private MailMessage mail;

    EmailSettings set;

    public async Task SendEmail(bool SendAndWait = false)
    {
        set = await rep.Get(AppData.GetAPIKey());
        mail = new MailMessage();
        mail.From = new MailAddress(set.SenderEmail, set.SenderName);
        Description = Description.Replace("[SenderName]", set.SenderName);
        URL = URL.Replace("[WebURL]", set.WebURL);

        foreach (var str in Sendto)
        {
            mail.To.Add(new MailAddress(str));
        }

        if (SendCC != null)
        {
            foreach (var str in SendCC)
            {
                mail.CC.Add(new MailAddress(str));
            }
        }

        if (SendBCC != null)
        {
            foreach (var str in SendBCC)
            {
                mail.Bcc.Add(new MailAddress(str));
            }
        }

        mail.Subject = Subject;
        mail.Priority = MailPriority.Normal;
        mail.IsBodyHtml = true;

        if (Attachment != "")
        {
            mail.Attachments.Add(new Attachment(Attachment));
        }

        string ImageTag = "";
        if (!(ImageURL == "" || ImageURL == null))
        {
            ImageTag = "<img width='100%' src='" + ImageURL + "' /> <br/><br/>";
        }


        mail.Body = PopulateBody(ToName, ImageTag, Description, ActionName, URL, set.SenderName, set.WebURL);

        try
        {
            SmtpClient smtp = new SmtpClient(set.EmailServer);
            smtp.Port = set.PortNumber;
            if (set.UseAuthentication)
            {
                smtp.Credentials = new NetworkCredential(set.SenderUsername, set.SenderPassword);
            }

            smtp.EnableSsl = set.UseSSL;

            if (SendAndWait)
            {
                SendSync(mail, smtp);
            }
            else
            {
                SendAsync(mail, smtp);
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }


    private async void SendAsync(MailMessage mail, SmtpClient smtp)
    {
        try
        {
            smtp.SendAsync(mail, "");
        }
        catch (Exception ex)
        {

            throw new Exception("Could not send emails to email addresses, " + ex.InnerException.Message);
        }
    }

    private void SendSync(MailMessage mail, SmtpClient smtp)
    {
        try
        {
            smtp.Send(mail);

            if (mail.Attachments != null)
            {
                for (Int32 i = mail.Attachments.Count - 1; i >= 0; i--)
                {
                    mail.Attachments[i].Dispose();
                }
                mail.Attachments.Clear();
                mail.Attachments.Dispose();
            }

            mail.Dispose();
            mail = null;
        }
        catch (Exception ex)
        {

            throw new Exception("Could not send emails to email addresses, " + ex.InnerException.Message);
        }
    }

    private string PopulateBody(string toName, string imageTag, string description, string actionName, string url, string webName, string webURL)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(GetUploadPath("EmailBody.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Preheader}", description);

        body = body.Replace("{ToName}", toName);
        body = body.Replace("{ImageTag}", imageTag);
        body = body.Replace("{Description}", description);

        body = body.Replace("{ActionName}", actionName);
        body = body.Replace("{URL}", url);

        body = body.Replace("{WebName}", webName);
        body = body.Replace("{WebURL}", webURL);

        return body;
    }

    public string GetUploadPath(string filename)
    {
        var ParerntPath = Directory.GetParent(environment.ContentRootPath).FullName;
        var FullPath = ParerntPath + "\\" + AppData.GetUplaodPath() + "\\" + filename;
        return FullPath;
    }

}