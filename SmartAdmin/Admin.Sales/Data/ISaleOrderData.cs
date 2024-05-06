using Sales.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface ISaleOrderData
    {
        Task<string> AddEdit(string APIKey, SaleOrder obj, string LogUserID);
        Task<string> EditPaidStatus(string APIKey, string ID, string PaidStatus, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<SaleOrder> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string CustomerID = "", string OrderStatus = "", string PaidStatus = "");
        Task<List<SaleOrder>> GetList(string APIKey, string KeyW = "", string CustomerID = "", string OrderStatus = "",string PaidStatus = "", int Page = 0, int PageSize = 99999);
    }
}