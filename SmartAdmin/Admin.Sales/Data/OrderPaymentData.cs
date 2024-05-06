using Sales.Models;
using DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data
{

    public class OrderPaymentData : IOrderPaymentData
    {
        #region Construction

        private readonly IDBAccess db;

        public OrderPaymentData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion

        public Task<string> AddEdit(string APIKey, OrderPayment obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "PaymentID",obj.PaymentID },
                { "OrderID",obj.OrderID },
                { "TransactionID",obj.TransactionID },
                { "TransactionType",obj.TransactionType },
                 { "ItemPrice",obj.ItemPrice  },
                { "PaymentStatus",obj.PaymentStatus},
                { "LogUserID",LogUserID }
            };

            string query = "sal.OrderPayment_AddEdit";
            return db.Execute(query, param);
        }

        public Task<OrderPayment> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.OrderPayment_Get";
            return db.Get<OrderPayment, dynamic>(query, param);
        }

        public Task<string> GetNumberFormat(string TableName, string KeyColumn)
        {
            var param = new Dictionary<string, object>
            {
             
                { "TableName",TableName },
                {"KeyColumn",KeyColumn }
            };

            string query = "adm.NumberFormat_Get";
            return db.Execute(query, param);
        }

    }
}
