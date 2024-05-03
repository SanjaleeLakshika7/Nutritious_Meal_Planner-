using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Catalog.Data;
using Catalog.Models;
using System.Collections.Generic;

// Helps to generate drop down lists (select lists)

namespace DropDown
{
    public class CatelogDDL : ICatelogDDL
    {
        #region Constructions

        private readonly ICategoryMainData mainCategories;
        private readonly ICategorySubData subCategories;
        private readonly IItemTypeData itemTypes;
        private readonly IItemSizeData SizeData;
        private readonly IIngredientsData IngredientsData;
        private readonly IBrandData brands;

        public CatelogDDL(ICategoryMainData mainCategories, ICategorySubData subCategories, IItemTypeData itemTypes, IBrandData brands, IItemSizeData SizeData, IIngredientsData IngredientsData)
        {
            this.mainCategories = mainCategories;
            this.subCategories = subCategories;
            this.itemTypes = itemTypes;
            this.SizeData = SizeData;
            this.IngredientsData = IngredientsData;
            this.brands = brands;
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

        public async Task<List<CategorySub>> CategoryMasterList()
        {
            return await subCategories.GetList(AppData.GetAPIKey());
        }

        public async Task<List<ItemSize>> ItemSizeList(string ItemID)
        {
            return await SizeData.GetList(AppData.GetAPIKey(), ItemID: ItemID);

        }
        public async Task<List<Ingredients>> IngredientsList(string ItemID)
        {
            var list = await IngredientsData.GetList(AppData.GetAPIKey(), ItemID: ItemID);
            return list;

        }

        public async Task<int> IngredientsCount(string ItemID)
        {
            var list = await IngredientsData.GetCount(AppData.GetAPIKey(), ItemID: ItemID);
            return list;

        }

        public async Task<ItemSize> ItemSizePriceVariationList(string ID)
        {
            var obj = await SizeData.Get(AppData.GetAPIKey(), ID: ID);
            return obj;



        }
    }
}

