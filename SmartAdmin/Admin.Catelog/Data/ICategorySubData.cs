using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface ICategorySubData
    {
        Task<string> AddEdit(string APIKey, CategorySub obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<CategorySub> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string CategoryMainID = "");
        Task<List<CategorySub>> GetList(string APIKey, string KeyW = "", string CategoryMainID = "", int Page = 0, int PageSize = 99999);
    }
}