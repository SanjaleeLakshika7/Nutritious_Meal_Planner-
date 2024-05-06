using Account.Models;
using Microsoft.AspNetCore.Http;

public class SessionData : ISessionData
{
    public User GetUser()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        return _context.HttpContext.Session.GetObject<User>("User");
    }
}
