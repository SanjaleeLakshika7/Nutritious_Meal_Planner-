using Microsoft.AspNetCore.Http;
using Account.Models;

public class Auth
{
    public static void CheckUser(string AccessCode = "")
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        if (_context.HttpContext.Session.GetObject<User>("User") == null)
        {
            var refUrl = string.Concat(
                        _context.HttpContext.Request.Scheme,
                        "://",
                        _context.HttpContext.Request.Host.ToUriComponent(),
                        _context.HttpContext.Request.PathBase.ToUriComponent(),
                        _context.HttpContext.Request.Path.ToUriComponent(),
                        _context.HttpContext.Request.QueryString.ToUriComponent());
            string ReturnURL = refUrl != null ? refUrl.ToString() : "";
            string AbsoluteURL = _context.HttpContext.Request.Path.ToString();
            if (AbsoluteURL == null) ReturnURL = "";
            if (AbsoluteURL == "/") ReturnURL = "";
            if (AbsoluteURL.ToLower().Contains("add")) ReturnURL = "";
            if (AbsoluteURL.ToLower().Contains("edit")) ReturnURL = "";
            if (AbsoluteURL.ToLower().Contains("login")) ReturnURL = "";

            if (ReturnURL != "")
            {
                ReturnURL = ReturnURL.Replace("&", "(and)");
                _context.HttpContext.Response.Redirect("/Account/Login?ReturnURL=" + ReturnURL);
            }
            else
            {
                _context.HttpContext.Response.Redirect("/Account/Login");
            }

        }
    }

    public static void CheckUserPartial(string AccessCode = "")
    {
        IHttpContextAccessor _context = new HttpContextAccessor();

        if (_context.HttpContext.Request.Cookies["ARem"] != null)
        {
            return;
        }

        if (_context.HttpContext.Session.GetObject<User>("User") == null)
        {
            _context.HttpContext.Response.Redirect("/Account/_Timeout");
        }
    }

    public static User GetUser()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        return _context.HttpContext.Session.GetObject<User>("User");
    }

    public static string GetUserID()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        var usr = _context.HttpContext.Session.GetObject<User>("User");
        if (usr != null)
        {
            return usr.UserID;
        }
        else
        {
            return "";
        }

        
    }

}
