using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Catalog.Models;
using Catalog.Data;

namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class CategoryMainController : Controller
    {
        #region Construction

        private readonly ICategoryMainData rep;

        public CategoryMainController(ICategoryMainData rep)
        {
            this.rep = rep;
        }

        #endregion

        public async Task<JsonResult> AddEdit(string APIKey, string CategoryMainID, string CategoryMainName, int DefaultSession, int ServiceCharge)
        {
            try
            {
                var obj = new CategoryMain();
                obj.CategoryMainID = CategoryMainID;
                obj.CategoryMainName = CategoryMainName;
                obj.DefaultSession = DefaultSession;
                obj.ServiceCharge = ServiceCharge;
                obj.CategoryMainID = await rep.AddEdit
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

        public async Task<JsonResult> GetCount(string APIKey, string KeyW = "")
        {
            try
            {
                var count = await rep.GetCount
                            (
                                APIKey: APIKey,
                                KeyW: KeyW
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

        public async Task<JsonResult> GetList(string APIKey, string KeyW = "", int Page = 0, int PageSize = 99999)
        {
            try
            {
                var objList = await rep.GetList
                            (
                                APIKey: APIKey,
                                KeyW: KeyW,
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
