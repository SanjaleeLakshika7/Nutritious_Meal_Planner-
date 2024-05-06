using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IImageData
    {
        Task<string> AddEdit(string APIKey, ImageUpload obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<List<ImageUpload>> GetList(string APIKey, string ID);
        Task<string> Temp_AddEdit(string APIKey, ImageUpload obj, string LogUserID);
        Task<string> Temp_Clear(string APIKey, string ID, string LogUserID);
        Task<List<ImageUpload>> Temp_GetList(string APIKey, string ID);
    }
}