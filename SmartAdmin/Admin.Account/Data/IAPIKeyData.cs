using Account.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface IAPIKeyData
    {
        Task<string> AddEdit(string APIKey, APIKey obj);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<APIKey> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string ActiveStatus = "");
        Task<List<APIKey>> GetList(string APIKey, string KeyW = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999);
        Task<string> Validate(string APIKey);
    }
}