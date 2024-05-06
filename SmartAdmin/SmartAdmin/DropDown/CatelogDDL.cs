using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Catalog.Data;
using Content.Data;
using Content.Models;
// Helps to generate drop down lists (select lists)

namespace DropDown
{
    public class CatelogDDL : ICatelogDDL
    {
        #region Constructions

        private readonly ICategoryMainData mainCategories;
        private readonly ICategorySubData subCategories;
        private readonly IItemTypeData itemTypes;
        private readonly IBrandData brands;
        private readonly ISettingsData settingrep;

        public CatelogDDL(ICategoryMainData mainCategories, ICategorySubData subCategories, IItemTypeData itemTypes, IBrandData brands,ISettingsData settingrep)
        {
            this.mainCategories = mainCategories;
            this.subCategories = subCategories;
            this.itemTypes = itemTypes;
            this.brands = brands;
            this.settingrep = settingrep;
        }

        #endregion

        public async Task<SelectList> ItemTypeList()
        {
            var objList = await itemTypes.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("ItemTypeID", "ItemTypeName");
        }

        public async Task<SelectList> CategoryMainList()
        {
            var objList = await mainCategories.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("CategoryMainID", "CategoryMainName");
        }

        public async Task<SelectList> CategorySubList()
        {
            var objList = await subCategories.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("CategorySubID", "CategorySubName");
        }

        public async Task<SelectList> CategorySubList(string CategoryMainID)
        {
            var objList = await subCategories.GetList(AppData.GetAPIKey(), CategoryMainID: CategoryMainID);
            return objList.ToSelectList("CategorySubID", "CategorySubName");
        }

        public async Task<SelectList> BrandList()
        {
            var objList = await brands.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("BrandID", "BrandName");
        }

        public async Task<string> GetImageSizeHeight()
        {
            var set = new Settings();
            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
            return set.SettingValue;
        }

        public async Task<string> GetImageSizeWidth()
        {
            var set = new Settings();
            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");
            return set.SettingValue;
        }
    }
}

