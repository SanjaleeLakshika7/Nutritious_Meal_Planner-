using Account.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface IUserData
    {
        Task<string> AddEdit(string APIKey, User obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<User> Get(string APIKey, string ID);
        Task<User> GetByEmail(string APIKey, string EmailAddress);
        Task<int> GetCount(string APIKey, string KeyW = "", string ActiveStatus = "");
        Task<List<User>> GetList(string APIKey, string KeyW = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999);
        Task<User> Login(string APIKey, string Username);
    }
}