using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Account.Models;
using Account.Data;

namespace MainWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailSettingsController : Controller
    {
        #region Construction

        private readonly IEmailSettingData rep;

        public EmailSettingsController(IEmailSettingData rep)
        {
            this.rep = rep;
        }

        #endregion


        public async Task<IActionResult> Index()
        {
            Auth.CheckUser();

            var obj = await rep.Get(APIKey: AppData.GetAPIKey());

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmailSettings obj)
        {
            Auth.CheckUser();

            try
            {
                obj.EmailServer = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj
                                    );

                return RedirectToAction("Index", "Home", new { Area = "Home" });
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }



    }
}
