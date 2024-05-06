using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Catalog.Models;

namespace Catalog.Data
{
    public class ImageData : IImageData
    {
        #region Construction

        private readonly IDBAccess db;

        public ImageData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, ImageUpload obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ImageID",obj.ImageID },
                { "RefID",obj.RefID },
                { "LogUserID",LogUserID }
            };

            string query = "cat.Image_AddEdit";
            return db.Execute(query, param);
        }

        public Task<string> Delete(string APIKey, string ID, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID },
                { "LogUserID",LogUserID }
            };

            string query = "cat.Image_Delete";
            return db.Execute(query, param);
        }

        public Task<List<ImageUpload>> GetList(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.Image_List";
            return db.GetList<ImageUpload, dynamic>(query, param);
        }


        // Temp

        public Task<string> Temp_AddEdit(string APIKey, ImageUpload obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ImageID",obj.ImageID },
                { "RefID",obj.RefID },
                { "LogUserID",LogUserID }
            };

            string query = "cat.Image_Temp_AddEdit";
            return db.Execute(query, param);
        }

        public Task<string> Temp_Clear(string APIKey, string ID, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID },
                { "LogUserID",LogUserID }
            };

            string query = "cat.Image_Temp_Clear";
            return db.Execute(query, param);
        }

        public Task<List<ImageUpload>> Temp_GetList(string APIKey, string ID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID }
            };

            string query = "cat.Image_Temp_List";
            return db.GetList<ImageUpload, dynamic>(query, param);
        }



    }
}
