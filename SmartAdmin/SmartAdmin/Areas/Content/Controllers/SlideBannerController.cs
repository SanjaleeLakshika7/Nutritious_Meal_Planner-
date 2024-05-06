using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using SmartAdmin.Models;
using Content.Models;
using Content.Data;
using System.IO;

namespace MainWeb.Areas.Catelog.Controllers
{
    [Area("Content")]
    public class SlideBannerController : Controller
    {
        #region Construction

        private readonly ISlideBannerData rep;
        private readonly IImageWork repWork;

        public SlideBannerController(ISlideBannerData rep, IImageWork repWork)
        {
            this.rep = rep;
            this.repWork = repWork;
        }

        #endregion


        public async Task<IActionResult> Index(string KeyW = "")
        {
            Auth.CheckUser();

            var obj = new SlideBannerSearchView();
            obj.KeyW = KeyW;
            obj.RecordCount = await rep.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank()
                                );
            obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

            return View(obj);
        }

        public IActionResult Add()
        {
            Auth.CheckUser();
            var obj = new SlideBannerEditView();
            obj.Data = new SlideBanner();

            try
            {
                obj.Data.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SlideBannerEditView obj)
        {
            Auth.CheckUser();
            try
            {
                if (obj.FileUpload == null)
                {
                    ViewData["ErrorMessage"] = "Please choose an image file for the banner";
                    return View(obj);
                }


                obj.Data.SlideBannerID = "";
                obj.Data.SlideBannerID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.Data,
                                       LogUserID: ""
                                    );

                if (obj.FileUpload != null)
                {
                    var filename = obj.Data.SlideBannerID + ".jpg";
                    var filePath = repWork.GetUploadPath(filename);
                    obj.FileUpload.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                return Redirect(obj.Data.ReturnURL);
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
            var obj = new SlideBannerEditView();
            obj.Data = await rep.Get(AppData.GetAPIKey(), id);

            try
            {
                obj.Data.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SlideBannerEditView obj)
        {
            Auth.CheckUser();
            try
            {
                obj.Data.SlideBannerID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.Data,
                                       LogUserID: ""
                                    );

                if (obj.FileUpload != null)
                {
                    var filename = obj.Data.SlideBannerID + ".jpg";
                    var filePath = repWork.GetUploadPath(filename);
                    obj.FileUpload.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                return Redirect(obj.Data.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }



        // Partial

        public async Task<ActionResult> _MainList(string KeyW = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }

        public async Task<ActionResult> _SlideBannerSelect(string KeyW = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }

        public async Task<ActionResult> _SlideBannerTick(string KeyW = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }

        // JSon

        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                if (id == null || id == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }


                //var emp = (Employee)Session["Employee"];

                var RetValue = await rep.Delete
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

        public async Task<JsonResult> GetList(string id)
        {
            try
            {
                if (id == null || id == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey()

                            );

                return Json(new { success = true, data = objList, responseText = "Deleted successfully" });

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
