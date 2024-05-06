using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Content.Data;
using Content.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DropDown
{
    public class PageMetaTag : IPageMetaTag
    {
        private readonly IMetaTagData rep;
        IHttpContextAccessor _context = new HttpContextAccessor();

        #region Constructions
            public PageMetaTag(IMetaTagData rep)
            {
                this.rep = rep;
            }
        #endregion

        public async Task<MetaTag> Get(string ID)
        {
            var obj = await rep.Get(AppData.GetAPIKey(), ID);
            return obj;
        }

        public string GetURL()
        {
            var PageHost = AppData.GetWebURL();
            return PageHost +  _context.HttpContext.Request.Path.ToString();
        }

        public string CheckFile(string ImageId ="")
        {
            if (String.IsNullOrEmpty(ImageId))
            {
                return "defaultimage.jpg";
            }

            if (File.Exists(@"D:\Dushman Projects\Smart Admin\EShop_Microsis\EShop\wwwroot\Uploads\" + ImageId + ".jpg") )
            {
                return ImageId+".jpg";
            }
     
            else {
                return "defaultimage.jpg";
            }
               
            
            
        }
    }
}
