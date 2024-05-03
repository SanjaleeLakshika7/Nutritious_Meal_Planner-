using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Models;
using Sales.Models;
using Catalog.Models;
using Microsoft.AspNetCore.Http;

public class Auth
{
    public static void CheckUser()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        if (_context.HttpContext.Session.GetObject<Customer>("Customer") == null)
        {
            var refUrl = _context.HttpContext.Request.GetTypedHeaders().Referer;
            string ReturnURL = refUrl != null ? refUrl.ToString() : "";
            string AbsoluteURL = _context.HttpContext.Request.Path.ToString();
            if (AbsoluteURL == null) ReturnURL = "";
            if (AbsoluteURL == "/") ReturnURL = "";
            if (AbsoluteURL.ToLower().Contains("add")) ReturnURL = "";
            if (AbsoluteURL.ToLower().Contains("edit")) ReturnURL = "";
            if (AbsoluteURL.ToLower().Contains("login")) ReturnURL = "";

            if (ReturnURL != "")
            {
                ReturnURL = ReturnURL.Replace("&", "(and)");
                _context.HttpContext.Response.Redirect("/Account/Login?ReturnURL=" + ReturnURL);
            }
            else
            {
                _context.HttpContext.Response.Redirect("/Account/Login");
            }

        }
    }

    public static void CheckUserPartial()
    {
        IHttpContextAccessor _context = new HttpContextAccessor();
        if (_context.HttpContext.Session.GetObject<Customer>("Customer") == null)
        {
            _context.HttpContext.Response.Redirect("/Account/_Timeout");
        }
    }

    public static void UpdatePrice(ref List<Item> ItemList)
    {
        var PriceType = "RT";
        IHttpContextAccessor _context = new HttpContextAccessor();
        if (_context.HttpContext.Session.GetObject<Customer>("Customer") != null)
        {
            var cus = _context.HttpContext.Session.GetObject<Customer>("Customer");
            PriceType = cus.PriceType;
        }

        if (PriceType == "RT")
        {
            ItemList.ToList().ForEach(c => c.SellingPrice = c.RetailPrice.Value);
        }
        else
        {
            ItemList.ToList().ForEach(c => c.SellingPrice = c.WholesalePrice.Value);
        }
    }

    public static void UpdatePrice(ref Item Item)
    {
        var PriceType = "RT";
        IHttpContextAccessor _context = new HttpContextAccessor();
        if (_context.HttpContext.Session.GetObject<Customer>("Customer") != null)
        {
            var cus = _context.HttpContext.Session.GetObject<Customer>("Customer");
            PriceType = cus.PriceType;
        }

        if (PriceType == "RT")
        {
            Item.SellingPrice = Item.RetailPrice.Value;
        }
        else
        {
            Item.SellingPrice = Item.WholesalePrice.Value;
        }
    }
}
