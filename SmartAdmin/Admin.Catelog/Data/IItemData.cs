using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IItemData
    {
        Task<string> AddEdit(string APIKey, Item obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<Item> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string IsNew = "", string IsFeatured = "",string IsSpecial = "", string ActiveStatus = "");
        Task<List<Item>> GetList(string APIKey, string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string IsNew = "", string IsFeatured = "",string IsSpecial ="", string ActiveStatus = "", string SortOrder = "", int Page = 0, int PageSize = 99999);
        Task<List<Item>> RelatedList(string APIKey, string ItemID = "", int MaxItem = 4);
        Task<string> APIDelete(string APIKey, string ItemID, string ActiveStatus, string LogUserID);
    }
}