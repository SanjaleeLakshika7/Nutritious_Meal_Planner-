using Content.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.Data
{
    public interface ISectionTextData
    {
        Task<string> AddEdit(string APIKey, SectionText obj, string LogUserID);
        Task<string> Delete(string APIKey, string ID, string LogUserID);
        Task<SectionText> Get(string APIKey, string ID);
        Task<int> GetCount(string APIKey, string KeyW = "");
        Task<List<SectionText>> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999);
    }
}