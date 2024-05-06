using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Content.Models;
using Content.Data;
using Catalog.Data;
using Catalog.Models;
using Account.Data;
using Account.Models;


namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        #region Construction

        private readonly IItemData repItem;
        private readonly ISlideBannerData repCont;
        private readonly ISectionTextData repSec;
        private readonly IEmailSender email;
        private readonly IEmailSender emailVisitor;
        private readonly IUserData repUser;

        public HomeController(IItemData repItem, ISlideBannerData repCont, ISectionTextData repSec, IEmailSender email, IEmailSender emailVisitor, IUserData repUser)
        {
            this.repItem = repItem;
            this.repCont = repCont;
            this.repSec = repSec;
            this.email = email;
            this.emailVisitor = emailVisitor;
            this.repUser = repUser;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var obj = new HomeView();
            try
            {
                obj.BannerList = await repCont.GetList(AppData.GetAPIKey());
                obj.FeaturedList = await repItem.GetList(AppData.GetAPIKey(), IsFeatured: "Y", ActiveStatus: "A");
                obj.NewList = await repItem.GetList(AppData.GetAPIKey(), IsNew: "Y", ActiveStatus: "A");

                Auth.UpdatePrice(ref obj.FeaturedList);
                Auth.UpdatePrice(ref obj.NewList);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to get records. " + ex.Message;
            }
            return View(obj);
        }

        public  IActionResult About()
        {
           

            return View();
        }


        public ActionResult Contact()
        {
            var obj = new ContactForm();
          
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactForm obj)
        {
            try
            {
                email.Subject = $"Website Contact - {obj.SenderSubject}";
                email.ToName = "Administrator";
                email.Description += "<br/>";


                email.Description += "Date: " + DateTime.Now.ToString();
                email.Description += "<br/>";
                email.Description += "<br/>";
                email.Description += obj.SenderName + " has contacted you via contact us page in nutriplanner.softwareengineer.lk";
                email.Description += "<br/>";
                email.Description += "<br/>";
                email.Description += "Contact Number: " + obj.SenderPhoneNo;
                email.Description += "<br/>";

                email.Description += "Email Address: " + obj.SenderEmail;
                email.Description += "<br/>";

                email.Description += "<br/>";
                email.Description += "Message :";
                email.Description += "<br/>";
                email.Description += obj.Description;
                email.Description += "<br/>";
                email.Description += "<br/>";

                email.ActionName = "nutriplanner.softwareengineer.lk";
                email.SystemURL = $"";

                email.Sendto = new List<string>();

                var UsrList = await repUser.GetList(AppData.GetAPIKey(), ActiveStatus: "A");
                bool ConfirmSend = false;
                foreach (var usr in UsrList)
                {
                    if (usr.EmailAddress != "")
                    {
                        email.Sendto.Add(usr.EmailAddress);
                        ConfirmSend = true;
                    }
                }

                if (ConfirmSend)
                {
                    await email.SendEmail(SendAndWait: true);
                }

                return await VistorMail(obj);
                // call another controlle methods
            }
            catch (Exception ex)
            {
                ViewBag.MsgTitle = "Error in Sending Message";
                ViewBag.MsgBody = ex.Message;
                ViewBag.Link = "Index";
                return View("~/Views/Shared/Message.cshtml");

            }

        }
        public async Task<IActionResult> VistorMail(ContactForm obj)
        {
            try
            {
                emailVisitor.Subject = $" Thank you for contacting Us";
                emailVisitor.ToName = obj.SenderName;
                emailVisitor.Description += "<br/>";

                emailVisitor.Description += "Date: " + DateTime.Now.ToString();
                emailVisitor.Description += "<br/>";

                emailVisitor.Description += "<br/>";
                emailVisitor.Description += "Your message has submitted successfully";
                emailVisitor.Description += "<br/>";

                emailVisitor.ActionName = "nutriplanner.softwareengineer.lk";
                emailVisitor.SystemURL = $"";

                emailVisitor.Sendto = new List<string>();
                emailVisitor.Sendto.Add(obj.SenderEmail);
                await emailVisitor.SendEmail(SendAndWait: true);

                ViewBag.MsgTitle = "Thank you for contacting us.";
                ViewBag.MsgBody = " Our expert support team will answer all your questions.";

                ViewBag.Link = "Index";
                return View("~/Views/Shared/Message.cshtml");

            }
            catch (Exception ex)
            {
                ViewBag.MsgTitle = "Error in Sending Message";
                ViewBag.MsgBody = ex.Message;
                ViewBag.Link = "Index";
                return View("~/Views/Shared/Message.cshtml");

            }

        }

        



        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult ReturnPolicy()
        {
            return View();
        }

    }
}
