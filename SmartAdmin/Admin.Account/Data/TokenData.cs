using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Models;
using DBAccess;

namespace Account.Data
{
    public class TokenData : ITokenData
    {
        #region Construction

        private readonly IDBAccess db;

        public TokenData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, Token obj)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },

                { "TokenID",obj.TokenID },
                { "TokenType",obj.TokenType },
                { "RefID",obj.RefID },
                { "TokenData",obj.TokenData },
                { "CreatedDate",obj.CreatedDate },
                { "ExpiryDate",obj.ExpiryDate },
                { "TokenStatus",obj.TokenStatus }
            };

            string query = "adm.Token_AddEdit";
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

            string query = "adm.SystemToken_Count";
            return db.GetCount(query, param);
        }

        public Task<string> Delete(string APIKey, string ID, string LogTokenID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "adm.Token_Delete";
            return db.Execute(query, param);
        }

        public Task<Token> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "adm.Token_Get";
            return db.Get<Token, dynamic>(query, param);
        }

        public Task<List<Token>> GetList(string APIKey, string KeyW = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ActiveStatus",ActiveStatus },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "adm.Token_List";
            return db.GetList<Token, dynamic>(query, param);
        }


    }
}
