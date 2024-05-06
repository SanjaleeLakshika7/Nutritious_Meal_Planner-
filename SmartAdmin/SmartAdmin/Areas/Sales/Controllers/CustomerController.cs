using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Sales.Models;
using Sales.Data;

namespace MainWeb.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class CustomerController : Controller
    {
        #region Construction

        private readonly ICustomerData rep;

        public CustomerController(ICustomerData rep)
        {
            this.rep = rep;
        }

        #endregion


        public async Task<IActionResult> Index(string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string ActiveStatus = "")
        {
            Auth.CheckUser();

            var obj = new CustomerSearchView();
            obj.KeyW = KeyW;
            obj.RecordCount = await rep.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank(),
                                    CustomerCategoryID: CustomerCategoryID.ToBlank(),
                                    CustomerGroupID: CustomerGroupID.ToBlank(),
                                    ActiveStatus: ActiveStatus.ToBlank()
                                );
            obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

            return View(obj);
        }

        public IActionResult Add()
        {
            Auth.CheckUser();

            var obj = new Customer();

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer obj)
        {
            Auth.CheckUser();

            try
            {
                obj.CustomerID = "";
                obj.LoginPassword = Crypto.Encrypt(obj.LoginPassword);
                obj.CustomerID = await rep.AddEdit
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

        public async Task<IActionResult> Edit(string id)
        {
            Auth.CheckUser();

            var obj = await rep.Get(AppData.GetAPIKey(), id);
            obj.LoginPassword = "";
            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer obj)
        {
            Auth.CheckUser();

            try
            {
                if (obj.LoginPassword != null)
                {
                    obj.LoginPassword = Crypto.Encrypt(obj.LoginPassword);
                }
                obj.CustomerID = await rep.AddEdit
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

        public async Task<ActionResult> _MainList(string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string ActiveStatus = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                CustomerCategoryID: CustomerCategoryID.ToBlank(),
                                CustomerGroupID: CustomerGroupID.ToBlank(),
                                ActiveStatus: ActiveStatus.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }

        public async Task<ActionResult> _CustomerSelect(string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string ActiveStatus = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                CustomerCategoryID: CustomerCategoryID.ToBlank(),
                                CustomerGroupID: CustomerGroupID.ToBlank(),
                                ActiveStatus: ActiveStatus.ToBlank(),
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

                //var emp = (Customer)Session["Customer"];

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
