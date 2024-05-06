using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IItemTypeData
    {
        Task<ItemType> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<ItemType>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}