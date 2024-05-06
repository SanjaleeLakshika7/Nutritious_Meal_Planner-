using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class CategoryMainData : ICategoryMainData
    {
        #region Construction

        private readonly IDBAccess db;

        public CategoryMainData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, CategoryMain obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CategoryMainID",obj.CategoryMainID },
                { "CategoryMainName",obj.CategoryMainName },
                { "DefaultSession",obj.DefaultSession },
                { "ServiceCharge",obj.ServiceCharge },
                { "LogUserID",LogUserID }
            };

            string query = "cat.CategoryMain_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "cat.CategoryMain_Count";
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

            string query = "cat.CategoryMain_Delete";
            return db.Execute(query, param);
        }

        public Task<CategoryMain> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.CategoryMain_Get";
            return db.Get<CategoryMain, dynamic>(query, param);
        }

        public Task<List<CategoryMain>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.CategoryMain_List";
            return db.GetList<CategoryMain, dynamic>(query, param);
        }



    }
}
