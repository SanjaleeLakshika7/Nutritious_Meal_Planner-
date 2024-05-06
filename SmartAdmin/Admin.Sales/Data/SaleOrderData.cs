using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class SaleOrderData : ISaleOrderData
    {
        #region Construction

        private readonly IDBAccess db;

        public SaleOrderData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, SaleOrder obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "OrderID",obj.OrderID },
                { "CustomerID",obj.CustomerID },
                { "ShipAttTo",obj.ShipAttTo },
                { "ShipAddressLine1",obj.ShipAddressLine1 },
                { "ShipAddressLine2",obj.ShipAddressLine2 },
                { "ShipCity",obj.ShipCity },
                { "ShipState",obj.ShipState },
                { "ShipPostalCode",obj.ShipPostalCode },
                { "ShipCountry",obj.ShipCountry },
                { "Remarks",obj.Remarks },
                { "OrderStatus",obj.OrderStatus },
                { "PaidStatus",obj.PaidStatus},
                {"NextRecurringDate" , obj.NextRecurringDate },
                 {"PeriodIndex" , obj.PeriodIndex },
                {"StopRecurringStatus", obj.StopRecurringStatus },
                {"RecurringSheduledStatus", obj.RecurringSheduledStatus },
                {"SheduledTime", obj.SheduledTime },
               
                { "LogUserID",LogUserID }
            };

            string query = "sal.SaleOrder_AddEdit";
            return db.Execute(query, param);
        }

        public Task<string> EditPaidStatus(string APIKey, string ID, string PaidStatus, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID },
                { "PaidStatus",PaidStatus },
                { "LogUserID",LogUserID }
            };

            string query = "sal.SaleOrder_EditPaidStatus";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string CustomerID = "", string OrderStatus = "" , string PaidStatus = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CustomerID",CustomerID },
                { "OrderStatus",OrderStatus },
                { "PaidStatus",PaidStatus}
            };

            string query = "sal.SaleOrder_Count";
            return db.GetCount(query, param);
        }

        public Task<string> Delete(string APIKey, string ID, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID },
                { "LogUserID",LogUserID }
            };

            string query = "sal.SaleOrder_Delete";
            return db.Execute(query, param);
        }

        public Task<SaleOrder> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.SaleOrder_Get";
            return db.Get<SaleOrder, dynamic>(query, param);
        }

        public Task<List<SaleOrder>> GetList(string APIKey, string KeyW = "", string CustomerID = "", string OrderStatus = "",string PaidStatus="", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CustomerID",CustomerID },
                { "OrderStatus",OrderStatus },
                { "PaidStatus",PaidStatus},
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.SaleOrder_List";
            return db.GetList<SaleOrder, dynamic>(query, param);
        }

    }
}
