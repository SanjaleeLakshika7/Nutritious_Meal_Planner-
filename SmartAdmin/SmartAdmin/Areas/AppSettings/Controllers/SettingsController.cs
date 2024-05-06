using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using SmartAdmin.Models;
using Content.Models;
using Content.Data;
using System.IO;

namespace MainWeb.Areas.AppSettings.Controllers
{
    [Area("AppSettings")]
    public class SettingsController : Controller
    {
        

        #region Construction

        private readonly ISettingsCategoryData rep;
        private readonly ISettingsData settingrep;

        public SettingsController(ISettingsCategoryData rep, ISettingsData settingrep)
        {
            this.rep = rep;
            this.settingrep = settingrep;
        }

        #endregion
        

       

        public async Task<ActionResult> Index(string KeyW = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUser();

            var objList = await rep.GetList
                           (
                               APIKey: AppData.GetAPIKey(),
                               KeyW: KeyW.ToBlank(),
                               Page: Page,
                               PageSize: PageSize
                           );

            return View(objList);

        }
        
        public async Task<IActionResult> EditGeneralSettings()
        {
            Auth.CheckUser();
            var obj = new GeneralSettings();

            var set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "gen_EShopURL");
            obj.gen_EShopURL = set.SettingValue;

            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "gen_SmartAdminURL");
            obj.gen_SmartAdminURL = set.SettingValue;

            return View(obj);

        }

        [HttpPost]
        public async Task<IActionResult> EditGeneralSettings(GeneralSettings obj)
        {
            Auth.CheckUser();

            try
            {
                await settingrep.Edit(
                        APIKey: AppData.GetAPIKey(),
                        SettingName: "gen_EShopURL",
                        SettingValue: obj.gen_EShopURL,
                        LogUserID:""
                    );

                await settingrep.Edit(
                        APIKey: AppData.GetAPIKey(),
                        SettingName: "gen_SmartAdminURL",
                        SettingValue: obj.gen_SmartAdminURL
                        ,
                        LogUserID: ""
                    );

                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }
        public async Task<IActionResult> EditCatalogSettings()
        {
            Auth.CheckUser();
            var obj = new CatalogSettings();

            var set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_RestrictUpdate");
            obj.cat_RestrictUpdate = set.SettingValue;

            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ItemNameDuplicateAllow");
            obj.cat_ItemNameDuplicateAllow = set.SettingValue;

            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");
            obj.cat_ImageSizeWidth = set.SettingValue;

            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
            obj.cat_ImageSizeHeight = set.SettingValue;

            return View(obj);




        }

        [HttpPost]
        public async Task<IActionResult> EditCatalogSettings(CatalogSettings obj)
        {
            Auth.CheckUser();

            try
            {
                await settingrep.Edit(
                        APIKey: AppData.GetAPIKey(),
                        SettingName: "cat_RestrictUpdate",
                        SettingValue: obj.cat_RestrictUpdate,
                        LogUserID: ""
                    );

                await settingrep.Edit(
                        APIKey: AppData.GetAPIKey(),
                        SettingName: "cat_ItemNameDuplicateAllow",
                        SettingValue: obj.cat_ItemNameDuplicateAllow
                        ,
                        LogUserID: ""
                    );

                await settingrep.Edit(
                       APIKey: AppData.GetAPIKey(),
                       SettingName: "cat_ImageSizeWidth",
                       SettingValue: obj.cat_ImageSizeWidth
                       ,
                       LogUserID: ""
                   );
                await settingrep.Edit(
                      APIKey: AppData.GetAPIKey(),
                      SettingName: "cat_ImageSizeHeight",
                      SettingValue: obj.cat_ImageSizeHeight,
                      LogUserID: ""
                  );
                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }

      
    }
}
