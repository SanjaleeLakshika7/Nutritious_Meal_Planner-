using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class DiscountSchemaData : IDiscountSchemaData
    {
        #region Construction

        private readonly IDBAccess db;

        public DiscountSchemaData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, DiscountSchema obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "DiscountSchemaID",obj.DiscountSchemaID },
                { "ItemID",obj.ItemID },
                { "MinimumQty",obj.MinimumQty },
                { "DiscountAmount",obj.DiscountAmount },
                { "DiscountRate",obj.DiscountRate },
                { "FreeQty",obj.FreeQty },
                { "GradeList",obj.GradeList },
                { "PriceTypeList",obj.PriceTypeList },
                { "BranchIDList",obj.BranchIDList },
                { "ActiveStatus",obj.ActiveStatus },
                { "LogUserID",LogUserID }
            };

            string query = "cat.DiscountSchema_AddEdit";
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

            string query = "cat.DiscountSchema_Count";
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

            string query = "cat.DiscountSchema_Delete";
            return db.Execute(query, param);
        }

        public Task<DiscountSchema> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.DiscountSchema_Get";
            return db.Get<DiscountSchema, dynamic>(query, param);
        }

        public Task<List<DiscountSchema>> GetList(string APIKey, string KeyW = "", string ItemID = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "ItemID",ItemID },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.DiscountSchema_List";
            return db.GetList<DiscountSchema, dynamic>(query, param);
        }

    }
}
