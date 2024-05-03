using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EShop.Models;
using Common;
using Catalog.Data;
using Catalog.Models;

namespace EShop.Controllers
{
    public class ItemController : Controller
    {
        #region Construction

        private readonly IItemData repItem;
        private readonly ICategoryMainData repCatMain;
        private readonly ICategorySubData repCatSub;
        private readonly IImageData repImage;
        private readonly IItemSizeData SizeData;

        public ItemController(IItemData repItem, ICategoryMainData repCatMain, ICategorySubData repCatSub, IImageData repImage, IItemSizeData SizeData)
        {
            this.repItem = repItem;
            this.repCatMain = repCatMain;
            this.repCatSub = repCatSub;
            this.SizeData = SizeData;
            this.repImage = repImage;
        }

        #endregion

        public async Task<IActionResult> Index(string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string SortOrder = "", int Page = 1)
        {
            var obj = new ItemSearchView();
            try
            {
                var StockAvailable = "";
                var ActiveStatus = "A";
                int PageSize = 40;

                obj.KeyW = KeyW;
                obj.CategoryMainID = CategoryMainID;
                obj.CategorySubID = CategorySubID;
                obj.BrandID = BrandID;
                obj.SortOrder = SortOrder;
                obj.Page = Page;
                obj.PageSize = PageSize;
                obj.RecordCount = await repItem.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank(),
                                    CategoryMainID: CategoryMainID.ToBlank(),
                                    CategorySubID: CategorySubID.ToBlank(),
                                    BrandID: BrandID.ToBlank(),
                                    StockAvailable: StockAvailable.ToBlank(),
                                    ActiveStatus: ActiveStatus.ToBlank()
                                );

                obj.DataList = await repItem.GetList
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank(),
                                    CategoryMainID: CategoryMainID.ToBlank(),
                                    CategorySubID: CategorySubID.ToBlank(),
                                    BrandID: BrandID.ToBlank(),
                                    StockAvailable: StockAvailable.ToBlank(),
                                    ActiveStatus: ActiveStatus.ToBlank(),
                                    SortOrder: SortOrder.ToBlank(),
                                    Page: Page,
                                    PageSize: PageSize
                                );

                Auth.UpdatePrice(ref obj.DataList);

                obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

                ViewData["Title"] = "All Items";

                if (CategorySubID.ToBlank() != "")
                {
                    var Subcat = await repCatSub.Get(AppData.GetAPIKey(), CategorySubID);
                    ViewData["Title"] = Subcat.CategorySubName;
                }
                else if (CategoryMainID.ToBlank() != "")
                {
                    var cat = await repCatMain.Get(AppData.GetAPIKey(), CategoryMainID);
                    ViewData["Title"] = cat.CategoryMainName;
                }
                else if (KeyW.ToBlank() != "")
                {
                    ViewData["Title"] = "Search for '" + KeyW +"'";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to get records. " + ex.Message;
            }
            return View(obj);
        }

        public async Task<IActionResult> Details(string id)
        {
            var obj = new ItemDetailsView();
            try
            {
                var item = await repItem.Get(AppData.GetAPIKey(), id);
                Auth.UpdatePrice(ref item);
                obj.Item = item;
                obj.ImageList = await repImage.GetList(AppData.GetAPIKey(), id);
                obj.RelatedList = await repItem.RelatedList(AppData.GetAPIKey(), id, 8);

                Auth.UpdatePrice(ref obj.RelatedList);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to get records. " + ex.Message;
            }
            return View(obj);
        }

        public async Task<JsonResult> Get(string ID)
        {
            try
            {
                var obj = await SizeData.Get
                            (
                                AppData.GetAPIKey(),
                                ID: ID
                            );

                return Json(new { success = true, responseText = obj.PriceVariation });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, responseText = ex.Message });
            }
        }
    }
}
