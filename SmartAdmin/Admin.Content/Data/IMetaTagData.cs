using Content.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.Data
{
    public interface IMetaTagData
    {
        Task<string> AddEdit(string APIKey, MetaTag obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<MetaTag> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<MetaTag>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}