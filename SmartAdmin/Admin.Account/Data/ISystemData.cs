using System.Threading.Tasks;

namespace Account.Data
{
    public interface ISystemData
    {
        Task<string> GetNextKey(string TableName, string KeyColumn);
    }
}