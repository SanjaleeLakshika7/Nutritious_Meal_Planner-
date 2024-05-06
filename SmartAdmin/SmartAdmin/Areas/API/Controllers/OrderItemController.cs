using Microsoft.AspNetCore.Mvc;
using Sales.Data;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class OrderItemController : Controller
    {
        #region Construction

        private readonly IOrderItemData rep;

        public OrderItemController(IOrderItemData rep)
        {
            this.rep = rep;
        }

        #endregion

        public async Task<JsonResult> AddEdit(string APIKey,string OrderItemID,string OrderID, string ItemID,double ItemPrice,double ItemDiscount,double ItemQty,string Remarks)
        {
            try
            {
                var obj = new OrderItem();
                obj.OrderItemID = OrderItemID;
                obj.OrderID = OrderID;
                obj.ItemID = ItemID;
                obj.ItemPrice = ItemPrice;
                obj.ItemDiscount = ItemDiscount;
                obj.ItemQty = ItemQty;
                obj.Remarks = Remarks;
             
                obj.OrderItemID = await rep.AddEdit
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

        public async Task<JsonResult> GetCount(string APIKey, string KeyW = "", string OrderID = "")
        {
            try
            {
                var count = await rep.GetCount
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                OrderID: OrderID
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

        public async Task<JsonResult> GetList(string APIKey, string KeyW = "", string OrderID = "", int Page = 0, int PageSize = 99999)
        {
            try
            {
                var objList = await rep.GetList
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                OrderID: OrderID,
                                Page: Page,
                                PageSize: PageSize
                            );

                return Json(new { success = true, ErrorMessage = "", Data = objList });

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
