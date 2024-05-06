using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

using Account.Data;
using Account.Models;

namespace SmartAdmin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KeySettingsController : Controller
    {
        #region Construction

        private readonly IAPIKeyData key;

        public KeySettingsController(IAPIKeyData key)
        {
            this.key = key;
        }

        #endregion
        public async Task<IActionResult> Index(string KeyW = "", string ActiveStatus = "")
        {
            Auth.CheckUser();

            var obj = new APIKeySearchView();
            obj.KeyW = KeyW;
            obj.RecordCount = await key.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank(),
                                    ActiveStatus: ActiveStatus.ToBlank()
                                );
            obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

            return View(obj);
        }

        public IActionResult Add()
        {
            Auth.CheckUser();

            var obj = new APIKey();

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Add(APIKey obj)
        {
            Auth.CheckUser();

            try
            {
                obj.KeyID = "";
                obj.KeyID = await key.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj
                                    );

                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Auth.CheckUser();

            var obj = await key.Get(AppData.GetAPIKey(), id);

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(APIKey obj)
        {
            Auth.CheckUser();

            try
            {
                obj.KeyID = await key.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj

                                    );

                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }

        public async Task<ActionResult> _MainList(string KeyW = "", string ActiveStatus = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await key.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                ActiveStatus: ActiveStatus.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }

        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                if (id == null || id == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                //var emp = (User)Session["User"];

                var RetValue = await key.Delete
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    ID: id,
                                    LogUserID: ""
                                );

                return Json(new { success = true, responseText = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, responseText = "Unable to delete record. " + ex.Message });
            }
        }
    }
}
