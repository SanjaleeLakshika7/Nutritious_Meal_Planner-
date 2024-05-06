using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IDiscountSchemaData
    {
        Task<string> AddEdit(string APIKey, DiscountSchema obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<DiscountSchema> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string ItemID = "");
        Task<List<DiscountSchema>> GetList(string APIKey, string KeyW = "", string ItemID = "", int Page = 0, int PageSize = 99999);
    }
}