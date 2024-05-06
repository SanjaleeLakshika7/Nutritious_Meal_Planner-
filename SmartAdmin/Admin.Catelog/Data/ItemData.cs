using System.Collections.Generic;
using System.Threading.Tasks;
 
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class ItemData : IItemData
    {
        #region Construction

        private readonly IDBAccess db;

        public ItemData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion

        // Basic

        public Task<string> AddEdit(string APIKey, Item obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },

                { "ItemID",obj.ItemID },
                { "ItemTypeID",obj.ItemTypeID },
                { "CategoryMainID",obj.CategoryMainID },
                { "CategorySubID",obj.CategorySubID },
                { "ItemCode",obj.ItemCode },
                { "ItemName",obj.ItemName },
                { "RetailPrice",obj.RetailPrice },
                { "WholesalePrice",obj.WholesalePrice },
                { "ItemCost",obj.ItemCost },
                { "BrandID",obj.BrandID },
                { "itemDescription",obj.itemDescription },
                { "StockAvailable",obj.StockAvailable },
                { "IsNew",obj.IsNew },
                { "IsFeatured",obj.IsFeatured },
                {"IsSpecial",obj.IsSpecial },
                { "ActiveStatus",obj.ActiveStatus },

                { "LogUserID",LogUserID }
            };

            string query = "cat.Item_AddEdit";
            return db.Execute(query, param);
        }

        public Task<string> APIDelete(string APIKey, string ItemID,string ActiveStatus, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ItemID",ItemID },
                { "ActiveStatus",ActiveStatus },

                { "LogUserID",LogUserID }
            };

            string query = "cat.Item_APIDelete";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string IsNew = "", string IsFeatured = "", string IsSpecial ="", string ActiveStatus = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CategoryMainID",CategoryMainID },
                { "CategorySubID",CategorySubID },
                { "BrandID",BrandID },
                { "StockAvailable",StockAvailable },
                { "IsNew",IsNew },
                { "IsFeatured",IsFeatured },
                {"IsSpecial",IsSpecial },
                { "ActiveStatus",ActiveStatus }
            };

            string query = "cat.Item_Count";
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

            string query = "cat.Item_Delete";
            return db.Execute(query, param);
        }

        public Task<Item> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.Item_Get";
            return db.Get<Item, dynamic>(query, param);
        }

        public Task<List<Item>> GetList(string APIKey, string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string IsNew = "", string IsFeatured = "", string IsSpecial="", string ActiveStatus = "", string SortOrder = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CategoryMainID",CategoryMainID },
                { "CategorySubID",CategorySubID },
                { "BrandID",BrandID },
                { "StockAvailable",StockAvailable },
                { "IsNew",IsNew },
                { "IsFeatured",IsFeatured },
                {"IsSpecial",IsSpecial },
                { "ActiveStatus",ActiveStatus },
                { "SortOrder",SortOrder },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.Item_List";
            return db.GetList<Item, dynamic>(query, param);
        }

        public Task<List<Item>> RelatedList(string APIKey, string ItemID = "", int MaxItem = 4)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ItemID",ItemID },
                { "MaxItem",MaxItem }
            };

            string query = "cat.Item_ListRelated";
            return db.GetList<Item, dynamic>(query, param);
        }


    }
}
