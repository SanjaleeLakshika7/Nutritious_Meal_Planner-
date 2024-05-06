using Content.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.Data
{
    public interface ISettingsCategoryData
    {
        Task<SettingsCategory> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<SettingsCategory>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}