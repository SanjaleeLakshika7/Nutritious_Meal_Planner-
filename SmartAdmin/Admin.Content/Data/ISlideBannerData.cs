using Content.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.Data
{
    public interface ISlideBannerData
    {
        Task<string> AddEdit(string APIKey, SlideBanner obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<SlideBanner> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<SlideBanner>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}