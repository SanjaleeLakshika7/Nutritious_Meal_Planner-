using Account.Data;
using Account.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Controllers
{
    public class AccountController : Controller
    {
        #region Construction

        private readonly IUserData rep;
        private readonly IEmailSender email;
        private readonly ITokenData repToken;

        public AccountController(IUserData rep, IEmailSender email, ITokenData repToken)
        {
            this.rep = rep;
            this.email = email;
            this.repToken = repToken;
        }

        #endregion

        public async Task<IActionResult> Login(string ReturnURL)
        {
            // Check for Cookie
            if (Request.Cookies["ARem"] != null)
            {
                var TokenID = Request.Cookies["ARem"];
                var tk = await repToken.Get(AppData.GetAPIKey(), TokenID);
                if (tk == null) goto SkipToLogin;
                if (tk.IsValid == false) goto SkipToLogin;

                var usr = await rep.Get(AppData.GetAPIKey(), tk.RefID);
                if (usr.ActiveStatus == "I") goto SkipToLogin;

                HttpContext.Session.SetObject("User", usr);

                if (ReturnURL != null && ReturnURL != "")
                {
                    return Redirect(ReturnURL);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

        SkipToLogin:
            var obj = new LoginView();
            if (ReturnURL != null)
            {
                obj.ReturnURL = ReturnURL.Replace("(and)", "&");
                ViewData["ErrorMessage"] = "Please login to continue";
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginView obj)
        {
            try
            {
                var usr = await rep.Login(AppData.GetAPIKey(), obj.Username);
                if (usr != null)
                {
                    if (Crypto.Encrypt(obj.Password) != usr.Password)
                    {
                        ViewData["ErrorMessage"] = "Invalid password. Please try again.";
                    }
                    else if (usr.ActiveStatus != "A")
                    {
                        ViewData["ErrorMessage"] = "This user us not active.";
                    }
                    else
                    {
                        HttpContext.Session.SetObject("User", usr);

                        if (obj.RememberMe)
                        {

                            await SaveCookie(usr.UserID);
                        }

                        if (obj.ReturnURL != null && obj.ReturnURL != "")
                        {
                            return Redirect(obj.ReturnURL);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid username. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to login. " + ex.Message;
            }

            return View(obj);
        }


        public IActionResult ForgotPassword()
        {
            var obj = new LoginView();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginView obj)
        {
            try
            {
                var usr = await rep.GetByEmail(AppData.GetAPIKey(), obj.EmailAddress);
                if (usr != null)
                {
                    var tk = new Token();
                    tk.TokenID = "";
                    tk.TokenType = "APwReset";
                    tk.RefID = usr.UserID;
                    tk.TokenData = "";
                    tk.CreatedDate = DateTime.Now;
                    tk.ExpiryDate = DateTime.Now.AddDays(2);
                    tk.TokenStatus = "A";

                    tk.TokenID = await repToken.AddEdit(AppData.GetAPIKey(), tk);

                    email.Subject = "SmartAdmin User Account Recovery";
                    email.ToName = usr.DisplayName;
                    email.Description += "Please click on below link/button to reset your password This link will expire after 48 hours";

                    email.ActionName = "Reset Password";
                    email.URL = $"{AppData.GetWebURL()}/Account/ResetPassword/{tk.TokenID}";

                    email.Sendto = new List<string>(new[] { usr.EmailAddress });
                    await email.SendEmail(SendAndWait: true);

                    ViewBag.MsgTitle = "";
                    ViewBag.MsgBody = "Password Reset link sent to your email address. Please use the link to reset your password.";
                    ViewBag.MsgBodySpam = "(Please check your spam/junk folder if the email is not in your inbox).";
                    return View("~/Views/Shared/Message.cshtml");
                }
                else
                {
                    ViewBag.ErrorMessage = "This email address is not regsitered.";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to login. " + ex.Message;
            }

            return View(obj);
        }

        public async Task<IActionResult> ResetPassword(string id)
        {
            var obj = new LoginView();

            try
            {
                var token = await repToken.Get(AppData.GetAPIKey(), id);
                if (token == null)
                {
                    ViewBag.MsgTitle = "Invalid Token or broken link";
                    ViewBag.MsgBody = "Please try again to send a new password reset link.";
                    
                    return View("~/Views/Shared/Message.cshtml");
                }
                else if (token.IsValid == false)
                {
                    ViewBag.MsgTitle = "Invalid Token or broken link";
                    ViewBag.MsgBody = "Please try again to send a new password reset link.";
                   
                    return View("~/Views/Shared/Message.cshtml");
                }
                else if (token.TokenType != "APwReset")
                {
                    ViewBag.MsgTitle = "Invalid toke type";
                    ViewBag.MsgBody = "Please try again to send a new password reset link.";
                    
                    return View("~/Views/Shared/Message.cshtml");
                }
                else
                {
                    var usr = await rep.Get(AppData.GetAPIKey(), token.RefID);
                    obj.UserID = usr.UserID;
                    obj.TokenID = token.TokenID;
                }
            }
            catch (Exception ex)
            {
                ViewBag.MsgTitle = "Error in token";
                ViewBag.MsgBody = ex.Message;

                return View("~/Views/Shared/Message.cshtml");
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(LoginView obj)
        {
            try
            {
                var usr = await rep.Get(AppData.GetAPIKey(), obj.UserID);
                usr.Password = Crypto.Encrypt(obj.Password);
                usr.UserID = await rep.AddEdit(AppData.GetAPIKey(), usr, "");

                var token = await repToken.Get(AppData.GetAPIKey(), obj.TokenID);
                token.TokenStatus = "CM";
                token.TokenID = await repToken.AddEdit(AppData.GetAPIKey(), token);

                ViewBag.MsgTitle = "Password Reset Success!";
                ViewBag.MsgBody = "Your account is now having the new password.";

                return View("~/Views/Shared/Message.cshtml");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to login. " + ex.Message;
            }

            return View(obj);
        }

        public IActionResult _Timeout(string ReturnURL)
        {

            ViewData["ReturnURL"] = ReturnURL;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Append("ARem", "", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            return RedirectToAction("Login", "Account");
        }


        // Supporting
        public async Task SaveCookie(string UserID)
        {
            var OldVal = HttpContext.Request.Cookies["ARem"];
            var TokenValue = "";
            if (OldVal != null)
            {
                var oldToken = await repToken.Get(AppData.GetAPIKey(), OldVal);
                if (oldToken != null)
                {
                    TokenValue = oldToken.TokenID;
                    goto SaveToken;
                }
                else
                {
                    TokenValue = "";
                    goto SaveToken;
                }
            }
            else
            {
                goto SaveToken;
            }


        SaveToken:
            var tk = new Token();
            tk.TokenID = TokenValue;
            tk.TokenType = "ARem";
            tk.RefID = UserID;
            tk.TokenData = "";
            tk.CreatedDate = DateTime.Now;
            tk.ExpiryDate = DateTime.Now.AddMonths(3);
            tk.TokenStatus = "A";

            tk.TokenID = await repToken.AddEdit(AppData.GetAPIKey(), tk);

            CookieOptions option = new CookieOptions();
            option.Expires = tk.ExpiryDate;
            Response.Cookies.Append("ARem", tk.TokenID, option);
        }


    }
}
