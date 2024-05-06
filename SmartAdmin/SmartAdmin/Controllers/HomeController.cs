using Catalog.Data;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sales.Data;
using Sales.Models;
using SmartAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Controllers
{
    public class HomeController : Controller
    {
        #region Construction
            private readonly ICustomerData customerData;
            private readonly ISaleOrderData saleOrderData;
            private readonly IItemData itemData;
            private readonly IOrderItemData repItem;
        private readonly IOrderActionData repAct;


        public HomeController(ICustomerData customerData, ISaleOrderData saleOrderData, IItemData itemData, IOrderItemData repItem, IOrderActionData repAct)
            {
                this.customerData = customerData;
                this.saleOrderData = saleOrderData;
                this.itemData = itemData;
                this.repItem = repItem;
            this.repAct = repAct;
            }
        #endregion
        public async Task<IActionResult> Index()
        {
            Auth.CheckUser();

            var objList = await saleOrderData.GetList
                           (
                               APIKey: AppData.GetAPIKey(),
                               KeyW: "",
                               CustomerID: "",
                               OrderStatus: "",
                               PaidStatus: "",
                               Page: 1,
                               PageSize: 9999999
                           );

            DateTime filterDate = DateTime.Now.Date;
            
            objList = objList
                .Where(order => order.NextRecurringDate.Date == filterDate )
                .ToList();

            // Create new data for next service based on schedule
            var newOrders = objList
                .Where(order => order.NextRecurringDate.Date == filterDate.Date && order.StopRecurringStatus == "No")
                .Select(async order =>
                {

                    var objpervious = new SaleOrder();
                    objpervious = order;
                    objpervious.StopRecurringStatus = "Yes";
                     
                    objpervious.OrderID = await saleOrderData.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: objpervious,
                                       LogUserID: ""
                                    );


                    var obj = new SaleOrderDetailsView();
                    obj.Order = await saleOrderData.Get(AppData.GetAPIKey(), order.OrderID);
                    obj.Customer = await customerData.Get(AppData.GetAPIKey(), obj.Order.CustomerID);
                    obj.ItemList = await repItem.GetList(AppData.GetAPIKey(), order.OrderID);
                    obj.ActionList = await repAct.GetList(AppData.GetAPIKey(), order.OrderID);

                    obj.Order.OrderID = "";
                    obj.Order.OrderDate = DateTime.Now;
                    obj.Order.OrderStatus = "P";
                    obj.Order.PaidStatus = "NP";
                    obj.Order.NextRecurringDate = DateTime.Now.AddDays(1);
                    obj.Order.StopRecurringStatus = "No";
                    obj.Order.RecurringSheduledStatus = "Yes";
 
                    obj.Order.OrderID = await saleOrderData.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.Order,
                                       LogUserID: ""
                                    );

                    if (obj.ItemList != null)
                    {
                        foreach (var ct in obj.ItemList)
                        {
                            var itm = new OrderItem();
                            itm.OrderItemID = "";
                            itm.OrderID = obj.Order.OrderID;
                            itm.ItemID = ct.ItemID;
                            itm.ItemPrice = ct.ItemPrice;
                            itm.ItemDiscount = ct.ItemDiscount;
                            itm.ItemQty = ct.ItemQty;
                            itm.Remarks = ct.Remarks;
                            itm.OrderItemID = await repItem.AddEdit
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

                        act.OrderActionID = await repAct.AddEdit
                                            (
                                               APIKey: AppData.GetAPIKey(),
                                               obj: act,
                                               LogUserID: ""
                                            );
                    }



                }).ToList();

            var obj = new DashboardInfo();







            obj.customer = await customerData.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: "".ToBlank(),
                                CustomerCategoryID: "".ToBlank(),
                                CustomerGroupID: "".ToBlank(),
                                ActiveStatus: "".ToBlank(),
                                Page: 1,
                                PageSize: 9999999
                            );

             obj.saleOrders = await saleOrderData.GetList
                           (
                               APIKey: AppData.GetAPIKey(),
                               KeyW: "".ToBlank(),
                               CustomerID: "".ToBlank(),
                               OrderStatus: "".ToBlank(),
                               PaidStatus: "".ToBlank(),
                               Page: 1,
                               PageSize: 99999
                           );

            obj.ItemCount = await itemData.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: "".ToBlank(),
                                    CategoryMainID: "".ToBlank(),
                                    CategorySubID: "".ToBlank(),
                                    BrandID: "".ToBlank(),
                                    StockAvailable: "".ToBlank(),
                                    ActiveStatus: "".ToBlank()
                                );
             
            var currentDate = DateTime.Now;
            var past12Months = Enumerable.Range(0, 12)
                .Select(i => currentDate.AddMonths(-i).Month);

            foreach (var month in past12Months)
            {
                switch (month)
                {
                    case 1: // January
                        obj.Jan = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 2: // February
                        obj.Feb = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 3: // March
                        obj.Mar = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 4: // April
                        obj.Apr = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 5: // May
                        obj.May = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 6: // June
                        obj.Jun = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 7: // July
                        obj.Jul = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 8: // August
                        obj.Aug = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 9: // September
                        obj.Sep = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 10: // October
                        obj.Oct = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 11: // November
                        obj.Nov = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                    case 12: // December
                        obj.Dec = obj.saleOrders.Count(o => o.OrderDate.Month == month);
                        break;
                }
            }
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
