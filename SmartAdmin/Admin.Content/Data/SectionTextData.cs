using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Content.Models;

namespace Content.Data
{
    public class SectionTextData : ISectionTextData
    {
        #region Construction

        private readonly IDBAccess db;

        public SectionTextData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, SectionText obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "SectionID",obj.SectionID },
                { "SectionName",obj.SectionName },
                { "SectionBodyText",obj.SectionBodyText },
                { "LogUserID",LogUserID }
            };

            string query = "con.SectionText_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "con.SectionText_Count";
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

            string query = "con.SectionText_Delete";
            return db.Execute(query, param);
        }

        public Task<SectionText> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "con.SectionText_Get";
            return db.Get<SectionText, dynamic>(query, param);
        }

        public Task<List<SectionText>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "con.SectionText_List";
            return db.GetList<SectionText, dynamic>(query, param);
        }

    }
}
