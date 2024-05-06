using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Models;
using DBAccess;

namespace Content.Data
{
    public class SettingsCategoryData : ISettingsCategoryData
    {
        #region Construction

        private readonly IDBAccess db;

        public SettingsCategoryData(IDBAccess db)
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

            string query = "adm.SettingsCategory_Count";
            return db.GetCount(query, param);
        }
        public Task<List<SettingsCategory>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "adm.SettingsCategory_List";
            return db.GetList<SettingsCategory, dynamic>(query, param);
        }

        public Task<SettingsCategory> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "con.SettingsCategory_Get";
            return db.Get<SettingsCategory, dynamic>(query, param);
        }
    }
}
