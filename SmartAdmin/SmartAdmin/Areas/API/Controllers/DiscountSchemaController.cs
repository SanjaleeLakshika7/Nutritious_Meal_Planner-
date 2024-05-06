using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Catalog.Models;
using Catalog.Data;


namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class DiscountSchemaController : Controller
    {
        #region Construction

        private readonly IDiscountSchemaData rep;
       

        public DiscountSchemaController(IDiscountSchemaData rep)
        {
            this.rep = rep;
          
        }

        #endregion

        public async Task<JsonResult> AddEdit(string APIKey, string DiscountSchemaID, string ItemID, double MinimumQty, double DiscountAmount, double DiscountRate,double FreeQty, string GradeList, string PriceTypeList,string BranchIDList,string ActiveStatus)
        {
            try
            {
                var obj = new DiscountSchema();
                obj.DiscountSchemaID = DiscountSchemaID;
                obj.ItemID = ItemID;
                obj.MinimumQty = MinimumQty;
                obj.DiscountAmount = DiscountAmount;
                obj.DiscountRate = DiscountRate;
                obj.FreeQty = FreeQty;
                obj.GradeList = GradeList;
                obj.PriceTypeList = PriceTypeList;
                obj.BranchIDList = BranchIDList;
                obj.ActiveStatus = ActiveStatus;

                obj.DiscountSchemaID = await rep.AddEdit
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

        public async Task<JsonResult> GetCount(string APIKey, string KeyW = "", string ItemID = "")
        {
            try
            {
                var count = await rep.GetCount
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                ItemID: ItemID
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
                var obj = await rep.Delete
                            (
                                APIKey: APIKey,
                                ID: ID,
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

        public async Task<JsonResult> GetList(string APIKey, string KeyW = "", string ItemID = "", int Page = 0, int PageSize = 99999)
        {
            try
            {
                var objList = await rep.GetList
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                ItemID: ItemID,
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
