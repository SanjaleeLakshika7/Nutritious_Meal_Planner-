using Content.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.Data
{
    public interface ISettingsData
    {
        Task<string> Edit(string APIKey, string SettingName, string SettingValue, string LogUserID);
        Task<int> GetCount(string APIKey, string KeyW = "", string CategoryCode = "");
        Task<List<Settings>> GetList(string APIKey, string KeyW = "", string CategoryCode = "", int Page = 0, int PageSize = 99999);
        Task<Settings> Get(string APIKey, string SettingName);

        Task<int> GetInt(string APIKey, string SettingName);
    }
}