using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IItemSizeData
    {
        Task<string> AddEdit(string APIKey, ItemSize obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<ItemSize> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string ItemID = "");
        Task<List<ItemSize>> GetList(string APIKey, string KeyW = "", string ItemID = "", int Page = 0, int PageSize = 99999);
    }
}