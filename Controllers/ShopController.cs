using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Catalog.Data;
using Catalog.Models;
using Sales.Data;
using Sales.Models;
using Account.Data;
using Account.Models;

namespace EShop.Controllers
{
    public class ShopController : Controller
    {
        #region Construction

        private readonly IItemData repItem;

        private readonly ISaleOrderData repSales;
        private readonly IOrderItemData repOrderItem;
        private readonly IOrderActionData repAction;
        private readonly ICustomerData repCus;
        private readonly IEmailSender email;
        private readonly IUserData repUser;
        private readonly ISessionData sessionData;


        public ShopController(IItemData repItem, ISaleOrderData repSales, IOrderItemData repOrderItem, IOrderActionData repAction, ICustomerData repCus, IEmailSender email, IUserData repUser, ISessionData sessionData)
        {
            this.repItem = repItem;
            this.repSales = repSales;
            this.repOrderItem = repOrderItem;
            this.repAction = repAction;
            this.repCus = repCus;
            this.email = email;
            this.repUser = repUser;
            this.sessionData = sessionData;
        }

        #endregion

        public IActionResult Index()
        {
            var objList = new List<CartItem>();
            try
            {
                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    objList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to view the cart. " + ex.Message;
            }
            return View(objList);
        }

        public IActionResult _Cart()
        {
            var objList = new List<CartItem>();
            try
            {
                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    objList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to view the cart. " + ex.Message;
            }
            return View(objList);
        }

        public async Task<JsonResult> CartAdd(string ItemID, double ItemPrice, double ItemQty, string Remarks = "")
        {
            try
            {
                if (ItemID == null || ItemID == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                var objList = new List<CartItem>();
                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    objList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }

                var ItemFound = false;

                foreach (var itm in objList)
                {
                    if (itm.Item.ItemID == ItemID && itm.Remarks == Remarks)
                    {
                        ItemFound = true;
                        itm.ItemQty += ItemQty;
                    }

                }

                if (ItemFound == false)
                {
                    var ct = new CartItem();
                    ct.Item = await repItem.Get(AppData.GetAPIKey(), ItemID);
                    ct.ItemPrice = ItemPrice;
                    ct.ItemQty = ItemQty;
                    ct.Remarks = Remarks;
                    objList.Add(ct);
                }

                HttpContext.Session.SetObject("Cart", objList);

                return Json(new { success = true, responseText = "Cart updated successfully" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, responseText = "Unable to update cart. " + ex.Message });
            }
        }

        public JsonResult CartUpdate(string ItemID, double ItemQty, string Remarks = "")
        {
            try
            {
                if (ItemID == null || ItemID == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                var objList = new List<CartItem>();
                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    objList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }

                foreach (var itm in objList)
                {
                    if (Remarks == null || Remarks == "")
                    {
                        if (itm.Item.ItemID == ItemID && Remarks == null || Remarks == "")
                        {
                            itm.ItemQty = ItemQty;
                        }
                    }
                    else
                    {

                        if (itm.Item.ItemID == ItemID && itm.Remarks == Remarks)
                        {
                            itm.ItemQty = ItemQty;
                        }
                    }
                }

                HttpContext.Session.SetObject("Cart", objList);

                return Json(new { success = true, responseText = "Cart updated successfully" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, responseText = "Unable to update cart. " + ex.Message });
            }
        }

        public JsonResult CartRemove(string ItemID, string Remarks)
        {
            try
            {
                if (ItemID == null || ItemID == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                var objList = new List<CartItem>();
                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    objList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }

                var newList = new List<CartItem>();

                foreach (var itm in objList)
                {
                    if (Remarks == null || Remarks == "")
                    {
                        if (itm.Item.ItemID != ItemID )
                        {
                            newList.Add(itm);
                        }
                    }
                    else
                    {

                        if (!(itm.Item.ItemID == ItemID && itm.Remarks == Remarks))
                        {
                            newList.Add(itm);
                        }
                    }


                }

                HttpContext.Session.SetObject("Cart", newList);

                return Json(new { success = true, responseText = "Cart updated successfully" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, responseText = "Unable to update cart. " + ex.Message });
            }
        }

        public IActionResult CartClear()
        {
            var objList = new List<CartItem>();
            HttpContext.Session.SetObject("Cart", objList);
            return RedirectToAction("Index", "Item");
        }


        // Check Out

        public IActionResult Checkout()
        {
            Auth.CheckUser();
            var obj = new CheckoutView();
            try
            {
                obj.Order = FillOrderCustomer();
                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    obj.CartList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to laod the order. " + ex.Message;
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutView obj)
        {
            Auth.CheckUser();
            try
            {
                obj.Order.OrderID = "";
                obj.Order.OrderDate = DateTime.Now;
                obj.Order.OrderStatus = "P";
                obj.Order.PaidStatus = "NP";

                if (obj.Order.PeriodIndex == "O")
                {
                    obj.Order.NextRecurringDate = DateTime.Now;
                    obj.Order.StopRecurringStatus = "Yes";
                    obj.Order.RecurringSheduledStatus = "Not";
                }
                else
                {
                    // Add one day to current date
                    obj.Order.NextRecurringDate = DateTime.Now.AddDays(1);
                    obj.Order.StopRecurringStatus = "No";
                    obj.Order.RecurringSheduledStatus = "Yes";
                }


                obj.Order.OrderID = await repSales.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.Order,
                                       LogUserID: ""
                                    );

                if (HttpContext.Session.GetObject<List<CartItem>>("Cart") != null)
                {
                    obj.CartList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                }

                foreach (var ct in obj.CartList)
                {
                    var itm = new OrderItem();
                    itm.OrderItemID = "";
                    itm.OrderID = obj.Order.OrderID;
                    itm.ItemID = ct.Item.ItemID;
                    itm.ItemPrice = ct.ItemPrice;
                    itm.ItemDiscount = ct.ItemDiscount;
                    itm.ItemQty = ct.ItemQty;
                    itm.Remarks = ct.Remarks;
                    itm.OrderItemID = await repOrderItem.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: itm,
                                       LogUserID: ""
                                    );
                }

                var act = new OrderAction();
                act.OrderActionID = "";
                act.OrderID = obj.Order.OrderID;
                act.ActionDate = DateTime.Now;
                act.ActionType = "P";
                act.Remarks = "";
                act.UserID = "";

                act.OrderActionID = await repAction.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: act,
                                       LogUserID: ""
                                    );

                SendSubmitEmail(obj);

                var objList = new List<CartItem>();
                HttpContext.Session.SetObject("Cart", objList);

                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to place the order. " + ex.Message;
            }

            return View(obj);
        }

        public SaleOrder FillOrderCustomer()
        {
            var obj = new SaleOrder();

            var cus = HttpContext.Session.GetObject<Customer>("Customer");

            obj.CustomerID = cus.CustomerID;
            obj.ShipAttTo = cus.CustomerDisplay;
            obj.ShipAddressLine1 = cus.AddressLine1;
            obj.ShipAddressLine2 = cus.AddressLine2;
            obj.ShipCity = cus.City;
            obj.ShipState = cus.State;
            obj.ShipPostalCode = cus.PostalCode;
            obj.ShipCountry = cus.Country;

            return obj;

        }

        // Supporting
        private async void SendSubmitEmail(CheckoutView obj)
        {
            var cus = HttpContext.Session.GetObject<Customer>("Customer");

            email.Subject = $"New Order - # {obj.Order.OrderID}";
            email.ToName = "Administrator";
            email.Description += "<br/>";
            email.Description += "<br/>";

            email.Description += "Order # " + obj.Order.OrderID;
            email.Description += "<br/>";

            email.Description += "Date: " + obj.Order.OrderDate.ToString();
            email.Description += "<br/>";

            email.Description += "Customer: " + cus.CustomerDisplay;
            email.Description += "<br/>";

            email.Description += "Mobile No: " + cus.TelephoneMobile;
            email.Description += "<br/>";
            email.Description += "Other No: " + cus.TelephoneOther;
            email.Description += "<br/>";

            email.Description += "<br/>";
            email.Description += "Delivery Address :";
            email.Description += "<br/>";
            email.Description += obj.Order.AddressDisplay;
            email.Description += "<br/>";
            email.Description += "<br/>";

            if (obj.Order.Remarks != null)
            {
                email.Description += "Remarks :";
                email.Description += "<br/>";
                email.Description += obj.Order.Remarks;
                email.Description += "<br/>";
                email.Description += "<br/>";
            }

            if (obj.CartList.Count > 0)
            {
                var TotalString = obj.CartList.Sum(item => item.LineTotal).ToString("N");
                email.Description += obj.CartList.Count().ToString() + " items";
                email.Description += "<br/>";
                email.Description += "Total: " + TotalString;
                email.Description += "<br/>";
            }
            else
            {
                email.Description += "(No items in the list)";
                email.Description += "<br/>";
            }

            email.Description += "<br/>";

            email.Description += "Respond to the order and start processing accordingly. If the order is not acceptable, please inform the customer about it. Login to SmartAdmin to view more details about the order.";
            email.Description += "<br/>";

            email.ActionName = "View Order";
            email.SystemURL = $"/Order/Details/{obj.Order.OrderID}";

            email.Sendto = new List<string>();

            var UsrList = await repUser.GetList(AppData.GetAPIKey(), ActiveStatus: "A");
            bool ConfirmSend = false;
            foreach (var usr in UsrList)
            {
                if (usr.EmailAddress != "")
                {
                    email.Sendto.Add(usr.EmailAddress);
                    ConfirmSend = true;
                }
            }

            if (ConfirmSend)
            {
                email.SendEmail();
            }

        }

        public async Task<IActionResult> Orders(string KeyW = "", string OrderStatus = "", int Page = 1, int PageSize = 99999)
        {

            await sessionData.CheckToken();
            var CusData = sessionData.GetUser();

            if (CusData != null)
            {
                var obj = new SaleOrderSearchView();
                obj.KeyW = KeyW;
                var objList = await repSales.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                CustomerID: CusData.CustomerID,
                                OrderStatus: OrderStatus.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

                return View(objList);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        public async Task<IActionResult> Details(string id)
        {

            var obj = new SaleOrderDetailsView();

            obj.ItemList = await repOrderItem.GetList(
                AppData.GetAPIKey(),
                KeyW: "",
                OrderID: id,
                Page: 0,
                PageSize: 99999


                );
            try
            {
                obj.OrderID = id;
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);




        }

    }
}
