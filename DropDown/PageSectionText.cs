using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Data;
using Content.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DropDown
{
    public class PageSectionText : IPageSectionText
    {
        private readonly ISectionTextData rep;
        IHttpContextAccessor _context = new HttpContextAccessor();

        #region Constructions
        public PageSectionText(ISectionTextData rep)
        {
            this.rep = rep;
        }
        #endregion

        public async Task<SectionText> Get(string ID)
        {
            var obj = await rep.Get(AppData.GetAPIKey(), ID);
            return obj;
        }

        public string GetURL()
        {
            var PageHost = AppData.GetWebURL();
            return PageHost + _context.HttpContext.Request.Path.ToString();
        }

    }
}
