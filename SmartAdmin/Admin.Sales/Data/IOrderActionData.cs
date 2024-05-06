using Sales.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface IOrderActionData
    {
        Task<string> AddEdit(string APIKey, OrderAction obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<OrderAction> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string OrderID = "");
        Task<List<OrderAction>> GetList(string APIKey, string KeyW = "", string OrderID = "", int Page = 0, int PageSize = 99999);
    }
}