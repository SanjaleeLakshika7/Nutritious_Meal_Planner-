using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Models;
using DBAccess;

namespace Account.Data
{
    public class UserData : IUserData
    {
        #region Construction

        private readonly IDBAccess db;

        public UserData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, User obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "UserID",obj.UserID },
                { "UserName",obj.Username },
                { "Password",obj.Password },
                { "DisplayName",obj.DisplayName },
                { "EmailAddress",obj.EmailAddress },
                { "ActiveStatus",obj.ActiveStatus },
                { "LogUserID",LogUserID }
            };

            string query = "adm.SystemUser_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string ActiveStatus = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "ActiveStatus",ActiveStatus }
            };

            string query = "adm.SystemUser_Count";
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

            string query = "adm.SystemUser_Delete";
            return db.Execute(query, param);
        }

        public Task<User> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "adm.SystemUser_Get";
            return db.Get<User, dynamic>(query, param);
        }

        public Task<User> GetByEmail(string APIKey, string EmailAddress)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "EmailAddress",EmailAddress }
            };

            string query = "adm.SystemUser_GetByEmail";
            return db.Get<User, dynamic>(query, param);
        }

        public Task<List<User>> GetList(string APIKey, string KeyW = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ActiveStatus",ActiveStatus },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "adm.SystemUser_List";
            return db.GetList<User, dynamic>(query, param);
        }

        public Task<User> Login(string APIKey, string Username)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "Username",Username }
            };

            string query = "adm.SystemUser_Login";
            return db.Get<User, dynamic>(query, param);
        }


    }
}
