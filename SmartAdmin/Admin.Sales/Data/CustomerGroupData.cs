using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class CustomerGroupData : ICustomerGroupData
    {
        #region Construction

        private readonly IDBAccess db;

        public CustomerGroupData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, CustomerGroup obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CustomerGroupID",obj.CustomerGroupID },
                { "CustomerGroupName",obj.CustomerGroupName },
                { "LogUserID",LogUserID }
            };

            string query = "sal.CustomerGroup_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "sal.CustomerGroup_Count";
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

            string query = "sal.CustomerGroup_Delete";
            return db.Execute(query, param);
        }

        public Task<CustomerGroup> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.CustomerGroup_Get";
            return db.Get<CustomerGroup, dynamic>(query, param);
        }

        public Task<List<CustomerGroup>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.CustomerGroup_List";
            return db.GetList<CustomerGroup, dynamic>(query, param);
        }

    }
}
