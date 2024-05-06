using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class CountryData : ICountryData
    {
        #region Construction

        private readonly IDBAccess db;

        public CountryData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, Country obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CountryCode",obj.CountryCode },
                { "CountryName",obj.CountryName },
                { "LogUserID",LogUserID }
            };

            string query = "sal.Country_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "sal.Country_Count";
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

            string query = "sal.Country_Delete";
            return db.Execute(query, param);
        }

        public Task<Country> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "sal.Country_Get";
            return db.Get<Country, dynamic>(query, param);
        }

        public Task<List<Country>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.Country_List";
            return db.GetList<Country, dynamic>(query, param);
        }

    }
}
