using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class ItemTypeData : IItemTypeData
    {
        #region Construction

        private readonly IDBAccess db;

        public ItemTypeData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "cat.ItemType_Count";
            return db.GetCount(query, param);
        }

        public Task<ItemType> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.ItemType_Get";
            return db.Get<ItemType, dynamic>(query, param);
        }

        public Task<List<ItemType>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.ItemType_List";
            return db.GetList<ItemType, dynamic>(query, param);
        }

    }
}
