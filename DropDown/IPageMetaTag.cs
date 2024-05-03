using Content.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DropDown
{
    public interface IPageMetaTag
    {
        Task<MetaTag> Get(string ID);
        string GetURL();
       
    }
}