using Account.Models;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface IEmailSettingData
    {
        Task<string> AddEdit(string APIKey, EmailSettings obj);
        Task<EmailSettings> Get(string APIKey);
    }
}