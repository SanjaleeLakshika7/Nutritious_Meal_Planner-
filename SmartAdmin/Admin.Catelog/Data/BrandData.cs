using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class BrandData : IBrandData
    {
        #region Construction

        private readonly IDBAccess db;

        public BrandData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, Brand obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "BrandID",obj.BrandID },
                { "BrandName",obj.BrandName },
                { "LogUserID",LogUserID }
            };

            string query = "cat.Brand_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "cat.Brand_Count";
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

            string query = "cat.Brand_Delete";
            return db.Execute(query, param);
        }

        public Task<Brand> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.Brand_Get";
            return db.Get<Brand, dynamic>(query, param);
        }

        public Task<List<Brand>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.Brand_List";
            return db.GetList<Brand, dynamic>(query, param);
        }

    }
}
