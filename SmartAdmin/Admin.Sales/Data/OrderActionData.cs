using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class OrderActionData : IOrderActionData
    {
        #region Construction

        private readonly IDBAccess db;

        public OrderActionData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, OrderAction obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "OrderActionID",obj.OrderActionID },
                { "OrderID",obj.OrderID },
                { "ActionDate",obj.ActionDate },
                { "ActionType",obj.ActionType },
                { "Remarks",obj.Remarks },
                { "UserID",obj.UserID },

                { "LogUserID",LogUserID }
            };

            string query = "sal.OrderAction_AddEdit";
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

            string query = "sal.OrderAction_Count";
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

            string query = "sal.OrderAction_Delete";
            return db.Execute(query, param);
        }

        public Task<OrderAction> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.OrderAction_Get";
            return db.Get<OrderAction, dynamic>(query, param);
        }

        public Task<List<OrderAction>> GetList(string APIKey, string KeyW = "", string OrderID = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "OrderID",OrderID },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.OrderAction_List";
            return db.GetList<OrderAction, dynamic>(query, param);
        }

    }
}
