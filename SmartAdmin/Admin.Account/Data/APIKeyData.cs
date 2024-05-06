using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Models;
using DBAccess;

namespace Account.Data
{
    public class APIKeyData : IAPIKeyData
    {
        #region Construction

        private readonly IDBAccess db;

        public APIKeyData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion

        public Task<string> AddEdit(string APIKey, APIKey obj)
        {
            var param = new Dictionary<string, object>
            {
                { "KeyID",obj.KeyID },

                { "KeyDetails",obj.KeyDetails },
                { "ActiveStatus",obj.ActiveStatus }
            };

            string query = "adm.APIKey_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string ActiveStatus = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "ActiveStatus",ActiveStatus}
            };

            string query = "adm.APIKey_Count";
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

            string query = "adm.APIKey_Delete";
            return db.Execute(query, param);
        }

        public Task<List<APIKey>> GetList(string APIKey, string KeyW = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                {"ActiveStatus",ActiveStatus },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "adm.APIKey_List";
            return db.GetList<APIKey, dynamic>(query, param);
        }

        public Task<APIKey> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "adm.APIKey_Get";
            return db.Get<APIKey, dynamic>(query, param);
        }

        public Task<string> Validate(string APIKey)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey }
            };

            string query = "adm.APIKey_Validate";
            return db.Execute(query, param);
        }
    }
}
