using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class CustomerCategoryData : ICustomerCategoryData
    {
        #region Construction

        private readonly IDBAccess db;

        public CustomerCategoryData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, CustomerCategory obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CustomerCategoryID",obj.CustomerCategoryID },
                { "CustomerCategoryName",obj.CustomerCategoryName },
                { "LogUserID",LogUserID }
            };

            string query = "sal.CustomerCategory_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "sal.CustomerCategory_Count";
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

            string query = "sal.CustomerCategory_Delete";
            return db.Execute(query, param);
        }

        public Task<CustomerCategory> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.CustomerCategory_Get";
            return db.Get<CustomerCategory, dynamic>(query, param);
        }

        public Task<List<CustomerCategory>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.CustomerCategory_List";
            return db.GetList<CustomerCategory, dynamic>(query, param);
        }

    }
}
