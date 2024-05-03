using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Sales.Models;
using Sales.Data;
using Account.Models;
using Account.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

// Helps to generate drop down lists (select lists)

public class SessionData : ISessionData
{
    private readonly ITokenData repToken;
    private readonly ICustomerData repCus;

    public SessionData(ITokenData repToken, ICustomerData repCus)
    {
        this.repToken = repToken;
        this.repCus = repCus;
    }

    public Customer GetUser()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        return _context.HttpContext.Session.GetObject<Customer>("Customer");

    }

    public async Task CheckToken()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();

        if (_context.HttpContext.Request.Cookies["Crem"] != null)
        {
            var TokenID = _context.HttpContext.Request.Cookies["Crem"];
            var tk = await repToken.Get(AppData.GetAPIKey(), TokenID);
            if (tk == null) return;
            if (tk.IsValid == false) return;

            var cus = await repCus.Get(AppData.GetAPIKey(), tk.RefID);
            if (cus.ActiveStatus == "I") return;

            _context.HttpContext.Session.SetObject("Customer", cus);


        }
    }
}
