using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Account.Models;
using Account.Data;

namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class SettingsController : Controller
    {
        #region Construction

        private readonly IAPIKeyData rep;

        public SettingsController(IAPIKeyData rep)
        {
            this.rep = rep;
        }

        #endregion

        public async Task<JsonResult> Validate(string APIKey)
        {
            try
            {
                var obj = await rep.Validate
                            (
                                APIKey: APIKey
                            );

                return Json(new { success = true, ErrorMessage="", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, ErrorMessage = ex.Message });
            }
        }

    }
}
