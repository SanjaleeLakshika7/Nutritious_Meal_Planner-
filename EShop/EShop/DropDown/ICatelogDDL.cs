using Catalog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DropDown
{
    public interface ICatelogDDL
    {
        Task<SelectList> BrandList();
        Task<SelectList> CategoryMainList();
        Task<List<CategorySub>> CategoryMasterList();
        Task<SelectList> CategorySubList();
        Task<SelectList> CategorySubList(string CategoryMainID);
        Task<SelectList> ItemTypeList();

        Task<List<ItemSize>> ItemSizeList(string ItemID);
        Task<ItemSize> ItemSizePriceVariationList(string ID);
        Task<List<Ingredients>> IngredientsList(string ItemID);
        Task<int> IngredientsCount(string ItemID);

    }
}