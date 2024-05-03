using Sales.Models;
using System.Threading.Tasks;

public interface ISessionData
{
    Task CheckToken();
    Customer GetUser();
}