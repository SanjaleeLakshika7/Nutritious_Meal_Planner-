using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Sales.Models;
using Sales.Data;
using Account.Models;
using System.Collections.Generic;

namespace MainWeb.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class SaleOrderController : Controller
    {
        #region Construction

        private readonly ISaleOrderData rep;
        private readonly ICustomerData repCus;
        private readonly IOrderItemData repItem;
        private readonly IOrderActionData repAct;
        private readonly IEmailSender email;

        public SaleOrderController(ISaleOrderData rep, ICustomerData repCus, IOrderItemData repItem, IOrderActionData repAct, IEmailSender email)
        {
            this.rep = rep;
            this.repCus = repCus;
            this.repItem = repItem;
            this.repAct = repAct;
            this.email = email;
        }

        #endregion


        public async Task<IActionResult> Index(string KeyW = "", string CustomerID = "", string OrderStatus = "",string PaidStatus = "")
        {
            Auth.CheckUser();
            var obj = new SaleOrderSearchView();
            obj.KeyW = KeyW;
            obj.RecordCount = await rep.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank(),
                                    CustomerID: CustomerID.ToBlank(),
                                    OrderStatus: OrderStatus.ToBlank(),
                                    PaidStatus: PaidStatus.ToBlank()
                                );
            obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

            return View(obj);
        }

        public IActionResult Add()
        {
            Auth.CheckUser();
            var obj = new SaleOrder();

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SaleOrder obj)
        {
            Auth.CheckUser();

            try
            {
                obj.OrderID = "";
                obj.OrderID = await rep.AddEdit
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

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaleOrder obj)
        {
            Auth.CheckUser();
            try
            {
                obj.OrderID = await rep.AddEdit
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


        public async Task<IActionResult> Details(string id)
        {
            Auth.CheckUser();
            var obj = new SaleOrderDetailsView();
            obj.Order = await rep.Get(AppData.GetAPIKey(), id);
            obj.Customer = await repCus.Get(AppData.GetAPIKey(), obj.Order.CustomerID);
            obj.ItemList = await repItem.GetList(AppData.GetAPIKey(), OrderID: id);
            obj.ActionList = await repAct.GetList(AppData.GetAPIKey(), OrderID: id);

            try
            {
                obj.OrderID = id;
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Details(SaleOrderDetailsView obj)
        {
            Auth.CheckUser();

            try
            {
                var act = new OrderAction();
                act.OrderActionID = "";
                act.OrderID = obj.OrderID;
                act.ActionDate = DateTime.Now;
                act.ActionType = obj.ActionType;
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

                if (obj.ActionType == "A")
                {
                    SendProcessEmail(obj.OrderID, obj.Remarks);
                }
                else if (obj.ActionType == "R")
                {
                    SendRejectEmail(obj.OrderID, obj.Remarks);
                }
                else if (obj.ActionType == "D")
                {
                    SendDeliveryEmail(obj.OrderID, obj.Remarks);
                }
                else if (obj.ActionType == "C")
                {
                    SendCompletedEmail(obj.OrderID, obj.Remarks);
                }
                else if (obj.ActionType == "N")
                {
                    SendCancelEmail(obj.OrderID, obj.Remarks);
                }

                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            

            return View(obj);
        }


        // Partial

        public async Task<ActionResult> _MainList(string KeyW = "", string CustomerID = "", string OrderStatus = "",string PaidStatus="", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                CustomerID: CustomerID.ToBlank(),
                                OrderStatus: OrderStatus.ToBlank(),
                                PaidStatus : PaidStatus.ToBlank(),
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

                //var emp = (SaleOrder)Session["SaleOrder"];

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

        public async Task<JsonResult> EditPaidStatus(string id, string OrderPaidStatus)
        {
            try
            {
                if (id == null || id == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                if (OrderPaidStatus == null || OrderPaidStatus == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                //var emp = (Customer)Session["Customer"];

                var RetValue = await rep.EditPaidStatus
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    ID: id,
                                    PaidStatus : OrderPaidStatus,
                                    LogUserID: ""
                                );

                return Json(new { success = true, responseText = "Changed successfully" });
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


        // Email Alerts

        private async void SendProcessEmail(string OrderID, string Remarks = "")
        {
            var obj = await rep.Get(AppData.GetAPIKey(), OrderID);
            var cus = await repCus.Get(AppData.GetAPIKey(), obj.CustomerID);

            email.Subject = $"Order # {OrderID} - Started Processing";
            email.ToName = cus.CustomerDisplay;
            email.Description += $"Good news! Your order # { OrderID } has started processing by [SenderName]. <br /><br />";
            email.Description += "However, order processing will take some time depending on the queue and availability.<br />";

            if (Remarks!= "")
            {
                email.Description += $"<br />Notes:<br />{ Remarks }<br />";

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
                email.Description += $"<br />Notes:<br />{ Remarks }<br />";

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
                email.Description += $"<br />Notes:<br />{ Remarks }<br />";

            }

            email.ActionName = "View Order";
            email.URL = $"[WebURL]/Shop/Details/{OrderID}";

            email.Sendto = new List<string>(new[] { cus.LoginEmail });
            await email.SendEmail();
        }

        private async void SendCompletedEmail(string OrderID, string Remarks = "")
        {
            var obj = await rep.Get(AppData.GetAPIKey(), OrderID);
            var cus = await repCus.Get(AppData.GetAPIKey(), obj.CustomerID);

            email.Subject = $"Order # {OrderID} - Completed";
            email.ToName = cus.CustomerDisplay;
            email.Description += $"Your order # {OrderID} has marked as completed by [SenderName]. <br /><br />";
            email.Description += $"You can contact [SenderName] if you do have any problem with this completed order.<br />";

            if (Remarks != "")
            {
                email.Description += $"<br />Notes:<br />{ Remarks }<br />";

            }

            email.ActionName = "View Order";
            email.URL = $"[WebURL]/Shop/Details/{OrderID}";

            email.Sendto = new List<string>(new[] { cus.LoginEmail });
            await email.SendEmail();
        }

        private async void SendCancelEmail(string OrderID, string Remarks = "")
        {
            var obj = await rep.Get(AppData.GetAPIKey(), OrderID);
            var cus = await repCus.Get(AppData.GetAPIKey(), obj.CustomerID);

            email.Subject = $"Order # {OrderID} - Cancelled";
            email.ToName = cus.CustomerDisplay;
            email.Description += $"Ops! Order # {OrderID} has cancelled by [SenderName]. <br /><br />";

            if (Remarks != "")
            {
                email.Description += $"<br />Notes:<br />{ Remarks }<br />";

            }

            email.ActionName = "View Order";
            email.URL = $"[WebURL]/Shop/Details/{OrderID}";

            email.Sendto = new List<string>(new[] { cus.LoginEmail });
            await email.SendEmail();
        }

       
    }
}
