using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class ItemSizeData : IItemSizeData
    {
        #region Construction

        private readonly IDBAccess db;

        public ItemSizeData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion

        public Task<string> AddEdit(string APIKey, ItemSize obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ItemSizeID",obj.ItemSizeID },
                { "ItemID",obj.ItemID },
                { "SizeName",obj.SizeName },
                { "PriceVariation",obj.PriceVariation },


                { "LogUserID",LogUserID }
            };

            string query = "cat.ItemSize_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string ItemID = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "ItemID",ItemID }
            };

            string query = "cat.ItemSize_Count";
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

            string query = "cat.ItemSize_Delete";
            return db.Execute(query, param);
        }

        public Task<ItemSize> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.ItemSize_Get";
            return db.Get<ItemSize, dynamic>(query, param);
        }

        public Task<List<ItemSize>> GetList(string APIKey, string KeyW = "", string ItemID = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "ItemID",ItemID },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.ItemSize_List";
            return db.GetList<ItemSize, dynamic>(query, param);
        }
    }
}
