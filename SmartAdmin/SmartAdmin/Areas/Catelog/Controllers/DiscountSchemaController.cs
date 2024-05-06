using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Catalog.Models;
using Catalog.Data;

namespace MainWeb.Areas.Catelog.Controllers
{
    [Area("Catelog")]
    public class DiscountSchemaController : Controller
    {
        #region Construction

        private readonly IDiscountSchemaData rep;
        private readonly IItemData items;

        public DiscountSchemaController(IDiscountSchemaData rep, IItemData items)
        {
            this.rep = rep;
            this.items = items;
        }

        #endregion


        public async Task<IActionResult> Index(string KeyW = "")
        {
            Auth.CheckUser();

            var obj = new DiscountSchemaSearchView();
            obj.KeyW = KeyW;
            obj.RecordCount = await rep.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank()
                                );
            obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

            return View(obj);
        }


        public async Task<IActionResult> Edit(string id)
        {
            Auth.CheckUser();

            var obj = new DiscountSchemaEditView();
            obj.ItemID = id;
            obj.DiscountSchema = new DiscountSchema();
            obj.DiscountSchemaList = await rep.GetList(AppData.GetAPIKey(), ItemID: id);

            var itm = await items.Get(AppData.GetAPIKey(), id);
            obj.ItemName = itm.ItemDisplay;

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DiscountSchemaEditView obj)
        {
            Auth.CheckUser();

            try
            {
                var ItemID = obj.ItemID;
                obj.DiscountSchema.ItemID = ItemID;

                if (obj.DiscountSchema.DiscountSchemaID == null) obj.DiscountSchema.DiscountSchemaID = "";
                obj.DiscountSchema.DiscountSchemaID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.DiscountSchema,
                                       LogUserID: ""
                                    );
                ModelState.Clear();
                obj.DiscountSchema = new DiscountSchema();
                obj.ItemID = ItemID;
                var itm = await items.Get(AppData.GetAPIKey(), ItemID);
                obj.ItemName = itm.ItemDisplay;
                obj.DiscountSchemaList = await rep.GetList(AppData.GetAPIKey(), ItemID: ItemID);

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }



        // Partial

        public async Task<ActionResult> _MainList(string KeyW = "", string ItemID = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                ItemID: ItemID.ToBlank(),
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
