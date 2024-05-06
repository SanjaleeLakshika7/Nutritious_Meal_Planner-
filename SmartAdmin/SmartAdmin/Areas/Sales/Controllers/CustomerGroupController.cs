using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Sales.Models;
using Sales.Data;

namespace MainWeb.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class CustomerGroupController : Controller
    {
        #region Construction

        private readonly ICustomerGroupData rep;

        public CustomerGroupController(ICustomerGroupData rep)
        {
            this.rep = rep;
        }

        #endregion


        public async Task<IActionResult> Index(string KeyW = "")
        {
            var obj = new CustomerGroupSearchView();
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
            var obj = new CustomerGroup();

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(string id)
        {
            Auth.CheckUser();
            var obj = await rep.Get(AppData.GetAPIKey(), id);

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerGroup obj)
        {
            Auth.CheckUser();
            try
            {
                obj.CustomerGroupID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj,
                                       LogUserID: ""
                                    );

                return Redirect(obj.ReturnURL);
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
    }
}
