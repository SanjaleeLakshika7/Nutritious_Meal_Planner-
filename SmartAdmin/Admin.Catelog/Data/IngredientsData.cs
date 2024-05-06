using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class IngredientsData : IIngredientsData
    {
        #region Construction

        private readonly IDBAccess db;

        public IngredientsData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, Ingredients obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "IngredientsID",obj.IngredientsID },
                { "ItemID",obj.ItemID },
                { "Caption",obj.Caption },
                { "Description",obj.Description },
                { "DisOrder",obj.DisOrder },

                { "LogUserID",LogUserID }
            };

            string query = "cat.Ingredients_AddEdit";
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

            string query = "cat.Ingredients_Count";
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

            string query = "cat.Ingredients_Delete";
            return db.Execute(query, param);
        }

        public Task<Ingredients> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.Ingredients_Get";
            return db.Get<Ingredients, dynamic>(query, param);
        }

        public Task<List<Ingredients>> GetList(string APIKey, string KeyW = "", string ItemID = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "ItemID",ItemID },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "cat.Ingredients_List";
            return db.GetList<Ingredients, dynamic>(query, param);
        }
    }


}
