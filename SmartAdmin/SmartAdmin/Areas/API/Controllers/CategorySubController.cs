using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Catalog.Models;
using Catalog.Data;

namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class CategorySubController : Controller
    {
        #region Construction

        private readonly ICategorySubData rep;

        public CategorySubController(ICategorySubData rep)
        {
            this.rep = rep;
        }

        #endregion
        public async Task<JsonResult> AddEdit(string APIKey, string CategorySubID, string CategorySubName, string CategoryMainID)
        {
            try
            {
                var obj = new CategorySub();
                obj.CategorySubID = CategorySubID;
                obj.CategorySubName = CategorySubName;
                obj.CategoryMainID = CategoryMainID;
                
                obj.CategorySubID = await rep.AddEdit
                            (
                                APIKey: APIKey,
                                obj: obj,
                                LogUserID: ""
                            );

                 return Json(new { success = true, ErrorMessage = "", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new {success = false, ErrorMessage = ex.Message });
            }
        }

        public async Task<JsonResult> GetCount(string APIKey, string KeyW = "", string CategoryMainID = "")
        {
            try
            {
                var count = await rep.GetCount
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                CategoryMainID : CategoryMainID
                            );

                return Json(new { success = true, ErrorMessage = "", Data = count });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new {success = false, ErrorMessage = ex.Message });
            }
        }

        public async Task<JsonResult> Delete(string APIKey, string ID)
        {
            try
            {
                var obj = await rep.Delete
                            (
                                APIKey: APIKey,
                                ID: ID,
                                LogUserID: ""
                            );

                 return Json(new { success = true, ErrorMessage = "", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new {success = false, ErrorMessage = ex.Message });
            }
        }

        public async Task<JsonResult> Get(string APIKey, string ID)
        {
            try
            {
                var obj = await rep.Get
                            (
                                 APIKey: APIKey,
                                ID: ID
                            );

                 return Json(new { success = true, ErrorMessage = "", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new {success = false, ErrorMessage = ex.Message });
            }
        }

        public async Task<JsonResult> GetList(string APIKey, string KeyW = "", string CategoryMainID = "", int Page = 0, int PageSize = 99999)
        {
            try
            {
                var objList = await rep.GetList
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
                                CategoryMainID : CategoryMainID,
                                Page: Page,
                                PageSize: PageSize
                            );

                return Json(new { success = true, ErrorMessage = "", Data = objList });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new {success = false, ErrorMessage = ex.Message });
            }
        }
    }
}
