using Sales.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface ICustomerGroupData
    {
        Task<string> AddEdit(string APIKey, CustomerGroup obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<CustomerGroup> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<CustomerGroup>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}