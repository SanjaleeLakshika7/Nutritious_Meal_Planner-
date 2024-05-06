using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class CategorySubData : ICategorySubData
    {
        #region Construction

        private readonly IDBAccess db;

        public CategorySubData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, CategorySub obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CategorySubID",obj.CategorySubID },
                { "CategorySubName",obj.CategorySubName },
                { "CategoryMainID",obj.CategoryMainID },
                { "LogUserID",LogUserID }
            };

            string query = "cat.CategorySub_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string CategoryMainID = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CategoryMainID",CategoryMainID }
            };

            string query = "cat.CategorySub_Count";
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

            string query = "cat.CategorySub_Delete";
            return db.Execute(query, param);
        }

        public Task<CategorySub> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.CategorySub_Get";
            return db.Get<CategorySub, dynamic>(query, param);
        }

        public Task<List<CategorySub>> GetList(string APIKey, string KeyW = "", string CategoryMainID = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CategoryMainID",CategoryMainID },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.CategorySub_List";
            return db.GetList<CategorySub, dynamic>(query, param);
        }


    }
}
