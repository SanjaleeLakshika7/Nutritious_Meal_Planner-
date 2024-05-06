using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class OrderItemData : IOrderItemData
    {
        #region Construction

        private readonly IDBAccess db;

        public OrderItemData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, OrderItem obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "OrderItemID",obj.OrderItemID },
                { "OrderID",obj.OrderID },
                { "ItemID",obj.ItemID },
                { "ItemPrice",obj.ItemPrice },
                { "ItemDiscount",obj.ItemDiscount },
                { "ItemQty",obj.ItemQty },
                { "Remarks",obj.Remarks},

                { "LogUserID",LogUserID }
            };

            string query = "sal.OrderItem_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string OrderID = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "OrderID",OrderID }
            };

            string query = "sal.OrderItem_Count";
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

            string query = "sal.OrderItem_Delete";
            return db.Execute(query, param);
        }

        public Task<OrderItem> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.OrderItem_Get";
            return db.Get<OrderItem, dynamic>(query, param);
        }

        public Task<List<OrderItem>> GetList(string APIKey, string KeyW = "", string OrderID = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "OrderID",OrderID },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.OrderItem_List";
            return db.GetList<OrderItem, dynamic>(query, param);
        }

    }
}
