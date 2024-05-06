using Account.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface ITokenData
    {
        Task<string> AddEdit(string APIKey, Token obj);
        Task<string> Delete(string APIKey, string ID, string LogTokenID);
        Task<Token> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "", string ActiveStatus = "");
        Task<List<Token>> GetList(string APIKey, string KeyW = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999);
    }
}