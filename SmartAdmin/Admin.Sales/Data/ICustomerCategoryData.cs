using Sales.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface ICustomerCategoryData
    {
        Task<string> AddEdit(string APIKey, CustomerCategory obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<CustomerCategory> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<CustomerCategory>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}