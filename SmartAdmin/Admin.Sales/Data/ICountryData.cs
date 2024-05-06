using Sales.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface ICountryData
    {
        Task<string> AddEdit(string APIKey, Country obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<Country> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<Country>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}