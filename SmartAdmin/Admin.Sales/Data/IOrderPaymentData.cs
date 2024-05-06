using Sales.Models;
using System.Threading.Tasks;

namespace Sales.Data
{
    public interface IOrderPaymentData
    {
        Task<string> AddEdit(string APIKey, OrderPayment obj, string LogUserID);
        Task<OrderPayment> Get(string APIKey, string ID);
        Task<string> GetNumberFormat( string TableName, string KeyColumn);
    }
}