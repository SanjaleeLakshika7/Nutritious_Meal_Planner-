using Account.Models;
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
    public class OrderController : Controller
    {
        #region Construction

        private readonly ISaleOrderData rep;
        private readonly IEmailSender email;
        private readonly ICustomerData repCus;
        private readonly IOrderActionData repAct;
        public OrderController(ISaleOrderData rep, IEmailSender email, ICustomerData repCus, IOrderActionData repAct)
        {
            this.rep = rep;
            this.email = email;
            this.repCus = repCus;
            this.repAct = repAct;
        }

        #endregion

        public async Task<JsonResult> AddEdit(string APIKey, string OrderID, string CustomerID, string ShipAttTo, string ShipAddressLine1, string ShipAddressLine2,string ShipCity, string ShipState, string ShipPostalCode, string ShipCountry, string Remarks,string OrderStatus)
        {
            try
            {
                var obj = new SaleOrder();
                obj.OrderID = OrderID;
                obj.CustomerID = CustomerID;
                obj.ShipAttTo = ShipAttTo;
                obj.ShipAddressLine1 = ShipAddressLine1;
                obj.ShipAddressLine2 = ShipAddressLine2;
                obj.ShipCity = ShipCity;
                obj.ShipState = ShipState;
                obj.ShipPostalCode = ShipPostalCode;
                obj.ShipCountry = ShipCountry;
                obj.Remarks = Remarks;
                obj.OrderStatus = OrderStatus;
                obj.OrderID = await rep.AddEdit
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

        public async Task<JsonResult> GetCount(string APIKey, string KeyW = "", string CustomerID ="", string OrderStatus = "")
        {
            try
            {
                var count = await rep.GetCount
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                CustomerID : CustomerID,
                                OrderStatus : OrderStatus
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

        public async Task<JsonResult> GetList(string APIKey, string KeyW = "", string CustomerID = "", string OrderStatus = "", int Page = 0, int PageSize = 99999)
        {
            try
            {
                var objList = await rep.GetList
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                CustomerID : CustomerID,
                                OrderStatus : OrderStatus,
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


        public async Task<JsonResult> OrderStart(string APIKey, string OrderID)
        {
            try
            {
                var obj = new SaleOrder();

                var objOld = await rep.Get
                            (
                                APIKey: APIKey,
                                ID: OrderID
                            );


                obj.OrderID = OrderID;
                obj.CustomerID = objOld.CustomerID;
                obj.ShipAttTo = objOld.ShipAttTo;
                obj.ShipAddressLine1 = objOld.ShipAddressLine1;
                obj.ShipAddressLine2 = objOld.ShipAddressLine2;
                obj.ShipCity = objOld.ShipCity;
                obj.ShipState = objOld.ShipState;
                obj.ShipPostalCode = objOld.ShipPostalCode;
                obj.ShipCountry = objOld.ShipCountry;
                obj.Remarks = objOld.Remarks;
                obj.OrderStatus = "A";

                var act = new OrderAction();
                act.OrderActionID = "";
                act.OrderID = obj.OrderID;
                act.ActionDate = DateTime.Now;
                act.ActionType = obj.OrderStatus;
                act.Remarks = obj.Remarks;

                if (HttpContext.Session.GetObject<User>("User") != null)
                {
                    var User = HttpContext.Session.GetObject<User>("User");
                    act.UserID = User.UserID;
                }

                act.OrderActionID = await repAct.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: act,
                                       LogUserID: ""
                                    );



                SendProcessEmail(obj.OrderID, obj.Remarks);
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

        public async Task<JsonResult> OrderReject(string APIKey, string OrderID)
        {
            try
            {
                var obj = new SaleOrder();

                var objOld = await rep.Get
                            (
                                APIKey: APIKey,
                                ID: OrderID
                            );


                obj.OrderID = OrderID;
                obj.CustomerID = objOld.CustomerID;
                obj.ShipAttTo = objOld.ShipAttTo;
                obj.ShipAddressLine1 = objOld.ShipAddressLine1;
                obj.ShipAddressLine2 = objOld.ShipAddressLine2;
                obj.ShipCity = objOld.ShipCity;
                obj.ShipState = objOld.ShipState;
                obj.ShipPostalCode = objOld.ShipPostalCode;
                obj.ShipCountry = objOld.ShipCountry;
                obj.Remarks = objOld.Remarks;
                obj.OrderStatus = "R";

          


                var act = new OrderAction();
                act.OrderActionID = "";
                act.OrderID = obj.OrderID;
                act.ActionDate = DateTime.Now;
                act.ActionType = obj.OrderStatus;
                act.Remarks = obj.Remarks;

                if (HttpContext.Session.GetObject<User>("User") != null)
                {
                    var User = HttpContext.Session.GetObject<User>("User");
                    act.UserID = User.UserID;
                }

                act.OrderActionID = await repAct.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: act,
                                       LogUserID: ""
                                    );
                SendRejectEmail(obj.OrderID, obj.Remarks);
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

        public async Task<JsonResult> OrderDeliver(string APIKey, string OrderID)
        {
            try
            {
                var obj = new SaleOrder();

                var objOld = await rep.Get
                            (
                                APIKey: APIKey,
                                ID: OrderID
                            );


                obj.OrderID = OrderID;
                obj.CustomerID = objOld.CustomerID;
                obj.ShipAttTo = objOld.ShipAttTo;
                obj.ShipAddressLine1 = objOld.ShipAddressLine1;
                obj.ShipAddressLine2 = objOld.ShipAddressLine2;
                obj.ShipCity = objOld.ShipCity;
                obj.ShipState = objOld.ShipState;
                obj.ShipPostalCode = objOld.ShipPostalCode;
                obj.ShipCountry = objOld.ShipCountry;
                obj.Remarks = objOld.Remarks;
                obj.OrderStatus = "D";

                var act = new OrderAction();
                act.OrderActionID = "";
                act.OrderID = obj.OrderID;
                act.ActionDate = DateTime.Now;
                act.ActionType = obj.OrderStatus;
                act.Remarks = obj.Remarks;

                if (HttpContext.Session.GetObject<User>("User") != null)
                {
                    var User = HttpContext.Session.GetObject<User>("User");
                    act.UserID = User.UserID;
                }

                act.OrderActionID = await repAct.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: act,
                                       LogUserID: ""
                                    );
                SendDeliveryEmail(obj.OrderID, obj.Remarks);
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


        private async void SendProcessEmail(string OrderID, string Remarks = "")
        {
            var obj = await rep.Get(AppData.GetAPIKey(), OrderID);
            var cus = await repCus.Get(AppData.GetAPIKey(), obj.CustomerID);

            email.Subject = $"Order # {OrderID} - Started Processing";
            email.ToName = cus.CustomerDisplay;
            email.Description += $"Good news! Your order # {OrderID} has started processing by [SenderName]. <br /><br />";
            email.Description += "However, order processing will take some time depending on the queue and availability.<br />";

            if (Remarks != "")
            {
                email.Description += $"<br />Notes:<br />{Remarks}<br />";

            }

            email.ActionName = "View Order";
            email.URL = $"[WebURL]/Shop/Details/{OrderID}";

            email.Sendto = new List<string>(new[] { cus.LoginEmail });
            await email.SendEmail();
        }

        private async void SendRejectEmail(string OrderID, string Remarks = "")
        {
            var obj = await rep.Get(AppData.GetAPIKey(), OrderID);
            var cus = await repCus.Get(AppData.GetAPIKey(), obj.CustomerID);

            email.Subject = $"Order # {OrderID} - Rejected";
            email.ToName = cus.CustomerDisplay;
            email.Description += $"Ops! Your order # {OrderID} is rejected by [SenderName]. <br /><br />";
            email.Description += $"You can contact [SenderName] for more details and to get instructions to place a new order correctly.<br />";

            if (Remarks != "")
            {
                email.Description += $"<br />Notes:<br />{Remarks}<br />";

            }

            email.ActionName = "View Order";
            email.URL = $"[WebURL]/Shop/Details/{OrderID}";

            email.Sendto = new List<string>(new[] { cus.LoginEmail });

            await email.SendEmail();
        }

        private async void SendDeliveryEmail(string OrderID, string Remarks = "")
        {
            var obj = await rep.Get(AppData.GetAPIKey(), OrderID);
            var cus = await repCus.Get(AppData.GetAPIKey(), obj.CustomerID);

            email.Subject = $"Order # {OrderID} - On Delivery";
            email.ToName = cus.CustomerDisplay;
            email.Description += $"Its almost done! Your order # {OrderID} is now on delivery. Keep in touch, you will receive telephone calls from {OrderID} for confirmations. <br /><br />";
            email.Description += $"If you did not receive the order delivery within ordinary time, contact [SenderName].<br />";
            email.Description += "Do not forget to mark the order as completed when you received the order successfully to you.<br />";

            if (Remarks != "")
            {
                email.Description += $"<br />Notes:<br />{Remarks}<br />";

            }

            email.ActionName = "View Order";
            email.URL = $"[WebURL]/Shop/Details/{OrderID}";

            email.Sendto = new List<string>(new[] { cus.LoginEmail });
            await email.SendEmail();
        }
    }
}
