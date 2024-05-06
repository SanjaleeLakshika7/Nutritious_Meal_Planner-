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
    public class BrandController : Controller
    {
        #region Construction

        private readonly IBrandData rep;

        public BrandController(IBrandData rep)
        {
            this.rep = rep;
        }

        #endregion

        public async Task<JsonResult> AddEdit(string APIKey, string BrandID, string BrandName)
        {
            try
            {
                var obj = new Brand();
                obj.BrandID = BrandID;
                obj.BrandName = BrandName;
                obj.BrandID = await rep.AddEdit
                            (
                                APIKey: APIKey,
                                obj:obj,
                                LogUserID:""
                            );

                return Json(new { success = true, ErrorMessage="", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, ErrorMessage = ex.Message });
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

                return Json(new { success = true, ErrorMessage="", Data = count });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, ErrorMessage = ex.Message });
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

                return Json(new { success = true, ErrorMessage="", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, ErrorMessage= ex.Message });
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

                return Json(new { success = true, ErrorMessage="", Data = obj });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, ErrorMessage= ex.Message });
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

                return Json(new { success = true, ErrorMessage="",Data = objList });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, ErrorMessage= ex.Message });
            }
        }
    }
}
