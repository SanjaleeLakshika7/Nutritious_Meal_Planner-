using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Content.Models;

namespace Content.Data
{
    public class MetaTagData : IMetaTagData
    {
        #region Construction

        private readonly IDBAccess db;

        public MetaTagData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, MetaTag obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "PageID",obj.PageID },
                { "PageName",obj.PageName },
                { "TitleTag",obj.TitleTag },
                { "DescriptionTag",obj.DescriptionTag },
                { "KeywordsTag",obj.KeywordsTag },
                { "OGTitleTag",obj.OGTitleTag },
                { "OGDescriptionTag",obj.OGDescriptionTag },
                { "OGImageTag",obj.OGImageTag },
                { "LogUserID",LogUserID }
            };

            string query = "con.MetaTag_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "con.MetaTag_Count";
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

            string query = "con.MetaTag_Delete";
            return db.Execute(query, param);
        }

        public Task<MetaTag> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "con.MetaTag_Get";
            return db.Get<MetaTag, dynamic>(query, param);
        }

        public Task<List<MetaTag>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "con.MetaTag_List";
            return db.GetList<MetaTag, dynamic>(query, param);
        }

    }
}
