using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IBrandData
    {
        Task<string> AddEdit(string APIKey, Brand obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<Brand> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<Brand>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}