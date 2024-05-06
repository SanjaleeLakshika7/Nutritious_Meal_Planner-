using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;
using Catalog.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Account.Data;

namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class ItemController : Controller
    {
        #region Construction

        private readonly IItemData rep;
        

        public ItemController(IItemData rep)
        {
            this.rep = rep;
           
        }

        #endregion

        public async Task<JsonResult> AddEdit(string APIKey, string ItemID, string ItemTypeID, string CategoryMainID, string CategorySubID, string ItemCode, string ItemName, double RetailPrice,
            double WholesalePrice,double ItemCost, string BrandID,string itemDescription,string StockAvailable,string IsNew,string IsFeatured,string IsSpecial ,string ActiveStatus)
        {
            try
            {
                var obj = new Item();
                obj.ItemID = ItemID;
                obj.ItemTypeID = ItemTypeID;
                obj.CategoryMainID = CategoryMainID;
                obj.CategorySubID = CategorySubID;

                obj.ItemCode = ItemCode;
                obj.ItemName = ItemName;

                obj.RetailPrice = RetailPrice;
                obj.WholesalePrice = WholesalePrice;

                obj.ItemCost = ItemCost;
                obj.BrandID = BrandID;
                obj.itemDescription = itemDescription;

                obj.StockAvailable = StockAvailable;
                obj.IsNew = IsNew;
                obj.IsFeatured= IsFeatured;
                obj.IsSpecial = IsSpecial;
                obj.ActiveStatus = ActiveStatus;
                obj.ItemID = await rep.AddEdit
                            (
                                APIKey: APIKey,
                                obj: obj,
                                LogUserID: ""
                            );

                return Json(new { success = true, ErrorMessage = "", Data = obj });

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

        public async Task<JsonResult> GetCount(string APIKey, string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string IsNew = "", string IsFeatured = "", string IsSpecial = "", string ActiveStatus = "")
        {
            try
            {
                var count = await rep.GetCount
                            (
                                APIKey: APIKey,
                                KeyW:KeyW ,
                                CategoryMainID:CategoryMainID ,
                                CategorySubID:CategorySubID ,
                                BrandID:BrandID ,
                                StockAvailable:StockAvailable ,
                                IsNew:IsNew ,
                                IsFeatured:IsFeatured ,
                                IsSpecial: IsSpecial,
                                ActiveStatus:ActiveStatus 
                               

                            );

                return Json(new { success = true, ErrorMessage = "", Data = count });

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

        public async Task<JsonResult> Delete(string APIKey, string ID)
        {
            try
            {

                var obj = await rep.APIDelete
                            (
                                APIKey: APIKey,
                                ItemID: ID,
                                ActiveStatus : "I",
                                LogUserID: ""
                            );

                return Json(new { success = true, ErrorMessage = "", Data = obj });

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

        public async Task<JsonResult> Get(string APIKey, string ID)
        {
            try
            {
                var obj = await rep.Get
                            (
                                 APIKey: APIKey,
                                ID: ID
                            );

                return Json(new { success = true, ErrorMessage = "", Data = obj });

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

        public async Task<JsonResult> GetList(string APIKey, string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string IsNew = "", string IsFeatured = "",string IsSpecial ="", string ActiveStatus = "", int Page = 0, int PageSize = 99999)
        {
            try
            {
                var objList = await rep.GetList
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                CategoryMainID: CategoryMainID,
                                CategorySubID: CategorySubID,
                                BrandID: BrandID,
                                StockAvailable: StockAvailable,
                                IsNew: IsNew,
                                IsFeatured: IsFeatured,
                                IsSpecial : IsSpecial,
                                ActiveStatus: ActiveStatus,
                                Page: Page,
                                PageSize: PageSize
                            );

                return Json(new { success = true, ErrorMessage="", Data = objList });

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
