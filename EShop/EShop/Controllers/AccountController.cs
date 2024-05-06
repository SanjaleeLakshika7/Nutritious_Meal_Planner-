using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Sales.Data;
using Sales.Models;
using Account.Data;
using Account.Models;
using DropDown;
using Microsoft.AspNetCore.Http;

namespace EShop.Controllers
{
    public class AccountController : Controller
    {
        #region Construction

        private readonly ICustomerData repCus;
        private readonly ITokenData repToken;
        private readonly IEmailSender email;
        private readonly ISessionData sessionData;


        public AccountController(ICustomerData repCus, IEmailSender email, ITokenData repToken, ISessionData sessionData)
        {
            this.repCus = repCus;
            this.email = email;
            this.repToken = repToken;
            this.sessionData = sessionData;
        }

        #endregion
        public async Task<IActionResult> Index()
        {
            await sessionData.CheckToken();
            var CusData = sessionData.GetUser();
            var obj = new Customer();
            if (CusData != null)
            {
                obj = await repCus.Get(AppData.GetAPIKey(), CusData.CustomerID);
                obj.LoginPassword = "";
                obj.checkLoginData = 1;
                try
                {
                    obj.ReturnURL = Request.Headers["Referer"].ToString();
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Login Error. " + ex.Message;
                }

            }
            else
            {

                return RedirectToAction("Login", "Account");

            }

            return View(obj);
        }
        public async Task<IActionResult> Login(string ReturnURL)
        {
            if (Request.Cookies["Crem"] != null)
            {
                var TokenID = Request.Cookies["Crem"];
                var tk = await repToken.Get(AppData.GetAPIKey(), TokenID);
                if (tk == null) goto SkipToLogin;
                if (tk.IsValid == false) goto SkipToLogin;

                var cus = await repCus.Get(AppData.GetAPIKey(), tk.RefID);
                if (cus.ActiveStatus == "I") goto SkipToLogin;

                HttpContext.Session.SetObject("Customer", cus);

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
                var cus = await repCus.Get(AppData.GetAPIKey(), LoginEmail: obj.LoginEmail);
                if (cus != null)
                {
                    if (Crypto.Encrypt(obj.Password) != cus.LoginPassword)
                    {
                        ViewData["ErrorMessage"] = "Invalid password. Please try again.";
                    }
                    else if (cus.ActiveStatus != "A")
                    {
                        ViewData["ErrorMessage"] = "This user is not active.";
                    }
                    else
                    {
                        HttpContext.Session.SetObject("Customer", cus);
                        if (obj.RememberMe)
                        {

                            await SaveCookie(cus.CustomerID);
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
                    ViewData["ErrorMessage"] = "Invalid login email. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to login. " + ex.Message;
            }

            return View(obj);
        }


        public IActionResult Register()
        {
            var obj = new Customer();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Customer obj)
        {
            try
            {
                obj.CustomerID = "";
                obj.CustomerCategoryID = "0000";
                obj.CustomerGroupID = "";
                obj.PriceType = "RT";
                obj.ShipAway = "N";
                obj.ActiveStatus = "A";
                obj.LoginPassword = Crypto.Encrypt(obj.LoginPassword);
                obj.CustomerID = await repCus.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj,
                                       LogUserID: ""
                                    );

                HttpContext.Session.SetObject("Customer", obj);
                await SaveCookie(obj.CustomerID);
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to register. " + ex.Message;
            }

            return View(obj);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Append("Crem", "", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            return RedirectToAction("Login", "Account");
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
                var usr = await repCus.GetByEmail(AppData.GetAPIKey(), obj.LoginEmail);
                if (usr != null)
                {
                    var tk = new Token();
                    tk.TokenID = "";
                    tk.TokenType = "CPwReset";
                    tk.RefID = usr.CustomerID;
                    tk.TokenData = "";
                    tk.CreatedDate = DateTime.Now;
                    tk.ExpiryDate = DateTime.Now.AddDays(2);
                    tk.TokenStatus = "A";

                    tk.TokenID = await repToken.AddEdit(AppData.GetAPIKey(), tk);

                    email.Subject = "Microsis User Account Recovery";
                    email.ToName = usr.FirstName +" "+ usr.LastName;
                    email.Description += "Please click on below link/button to reset your password This link will expire after 48 hours";

                    email.ActionName = "Reset Password"; ;
                    email.SystemURL = $"/Account/ResetPassword/{tk.TokenID}";

                    email.Sendto = new List<string>(new[] { usr.LoginEmail });
                    await email.SendEmail(SendAndWait: true);

                    ViewBag.MsgTitle = "Password Reset";
                    ViewBag.MsgBody = "Password Reset link sent to your email address. Please use the link to reset your password.";
                    ViewBag.MsgBodySpam = "(Please check your spam/junk folder if the email is not in your inbox).";
                    ViewBag.Link = "Login";

                    return View("~/Views/Shared/Message.cshtml");
                }
                else
                {
                    ViewData["ErrorMessage"] = "This email address is not regsitered.";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
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
                else if (token.TokenType != "CPwReset")
                {
                    ViewBag.MsgTitle = "Invalid toke type";
                    ViewBag.MsgBody = "Please try again to send a new password reset link.";

                    return View("~/Views/Shared/Message.cshtml");
                }
                else
                {
                    var usr = await repCus.Get(AppData.GetAPIKey(), token.RefID);
                    obj.UserID = usr.CustomerID;
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
                var usr = await repCus.Get(AppData.GetAPIKey(), obj.UserID);
                usr.LoginPassword = Crypto.Encrypt(obj.Password);
                usr.CustomerID = await repCus.AddEdit(AppData.GetAPIKey(), usr, "");

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

        // Supporting
        public async Task SaveCookie(string CustomerID)
        {
            var OldVal = HttpContext.Request.Cookies["Crem"];
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
            tk.TokenType = "Crem";
            tk.RefID = CustomerID;
            tk.TokenData = "";
            tk.CreatedDate = DateTime.Now;
            tk.ExpiryDate = DateTime.Now.AddMonths(3);
            tk.TokenStatus = "A";

            tk.TokenID = await repToken.AddEdit(AppData.GetAPIKey(), tk);

            CookieOptions option = new CookieOptions();
            option.Expires = tk.ExpiryDate;
            Response.Cookies.Append("Crem", tk.TokenID, option);
        }

        public async Task<IActionResult> EditProfile()
        {
            await sessionData.CheckToken();
            var CusData = sessionData.GetUser();
            var obj = new Customer();
            if (CusData != null)
            {
                obj = await repCus.Get(AppData.GetAPIKey(), CusData.CustomerID);
                obj.LoginPassword = "";
                obj.checkLoginData = 1;
                try
                {
                    obj.ReturnURL = Request.Headers["Referer"].ToString();
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Login Error. " + ex.Message;
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Customer obj)
        {
            try
            {
                obj.CustomerID = await repCus.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj,
                                       LogUserID: ""
                                    );

                return Redirect("~/Account/Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }

        public async Task<IActionResult> EditPassword()
        {
            await sessionData.CheckToken();
            var CusData = sessionData.GetUser();

            if (CusData == null)
            {
                return RedirectToAction("Login", "Account");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(Customer obj)
        {
            await sessionData.CheckToken();
            var CusData = sessionData.GetUser();
            string OldPasswordCheck;
            string NewPassword;
            if (CusData != null)
            {
                try
                {
                    var usr = await repCus.Get(AppData.GetAPIKey(), CusData.CustomerID);
                    OldPasswordCheck = Crypto.Encrypt(obj.OldPassword);
                    if (usr.LoginPassword == OldPasswordCheck)
                    {
                        NewPassword = Crypto.Encrypt(obj.LoginPassword);
                        await repCus.EditPassword
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       CustomerID: usr.CustomerID,
                                       LoginPassword: NewPassword,
                                       LogUserID: ""
                                    );

                        return Redirect("~/Account/Index");

                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Old Password is incorrect ";
                    }

                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Unable to login. " + ex.Message;
                }

            }


            return View(obj);
        }

    }
}
