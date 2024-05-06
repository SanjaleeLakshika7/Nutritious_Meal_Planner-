using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Content.Models;

namespace Content.Data
{
    public class SlideBannerData : ISlideBannerData
    {
        #region Construction

        private readonly IDBAccess db;

        public SlideBannerData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, SlideBanner obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "SlideBannerID",obj.SlideBannerID },
                { "Title",obj.Title },
                { "Description",obj.Description },
                { "ButtonName",obj.ButtonName },
                { "ButtonLink",obj.ButtonLink },
                { "ActiveStatus",obj.ActiveStatus },
                { "LogUserID",LogUserID }
            };

            string query = "con.SlideBanner_AddEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW }
            };

            string query = "con.SlideBanner_Count";
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

            string query = "con.SlideBanner_Delete";
            return db.Execute(query, param);
        }

        public Task<SlideBanner> Get(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "con.SlideBanner_Get";
            return db.Get<SlideBanner, dynamic>(query, param);
        }

        public Task<List<SlideBanner>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "con.SlideBanner_List";
            return db.GetList<SlideBanner, dynamic>(query, param);
        }

    }
}
