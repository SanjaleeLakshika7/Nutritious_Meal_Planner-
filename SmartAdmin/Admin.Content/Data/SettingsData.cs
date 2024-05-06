using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Models;
using DBAccess;

namespace Content.Data
{
    public class SettingsData : ISettingsData
    {
        #region Construction

        private readonly IDBAccess db;

        public SettingsData(IDBAccess db)
        {
            this.db = db;
        }
        #endregion

        public Task<string> Edit(string APIKey, string SettingName , string SettingValue, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "SettingName",SettingName },
                { "SettingValue",SettingValue },
                { "LogUserID",LogUserID }
            };

            string query = "adm.Settings_Edit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string CategoryCode = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CategoryCode",CategoryCode }
            };

            string query = "adm.Settings_Count";
            return db.GetCount(query, param);
        }

        public Task<Settings> Get(string APIKey, string SettingName)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "SettingName",SettingName }
            };

            string query = "adm.Settings_Get";
            return db.Get<Settings, dynamic>(query, param);
        }

        public Task<int> GetInt(string APIKey, string SettingName)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "SettingName",SettingName }
            };

            string query = "adm.Settings_Get";
            return db.GetCount(query, param);
        }

        public Task<List<Settings>> GetList(string APIKey, string KeyW = "", string CategoryCode = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CategoryCode",CategoryCode },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "adm.Settings_List";
            return db.GetList<Settings, dynamic>(query, param);
        }
    }
}
