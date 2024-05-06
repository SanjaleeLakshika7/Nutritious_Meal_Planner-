using Sales.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface ICustomerData
    {
        Task<string> AddEdit(string APIKey, Customer obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<Customer> Get(string APIKey, string ID = "", string LoginEmail = "", string FacebookID = "", string GoogleID = "");
        Task<int> GetCount(string APIKey, string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string PriceType = "", string ActiveStatus = "");
        Task<List<Customer>> GetList(string APIKey, string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string PriceType = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999);
        Task<Customer> GetByEmail(string APIKey, string EmailAddress);

        Task<string> EditPassword(string APIKey, string CustomerID, string LoginPassword, string LogUserID);
    }
}