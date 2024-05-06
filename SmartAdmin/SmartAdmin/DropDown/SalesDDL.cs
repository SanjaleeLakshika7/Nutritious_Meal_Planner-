using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Sales.Data;

// Helps to generate drop down lists (select lists)

namespace DropDown
{
    public class SalesDDL : ISalesDDL
    {
        #region Constructions

        private readonly ICustomerCategoryData customerCategories;
        private readonly ICustomerGroupData customerGroups;
        private readonly ICountryData countries;

        public SalesDDL(ICustomerCategoryData customerCategories, ICustomerGroupData customerGroups, ICountryData countries)
        {
            this.customerCategories = customerCategories;
            this.customerGroups = customerGroups;
            this.countries = countries;
        }

        #endregion

        public async Task<SelectList> CustomerCategoryList()
        {
            var objList = await customerCategories.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("CustomerCategoryID", "CustomerCategoryName");
        }

        public async Task<SelectList> CustomerGroupList()
        {
            var objList = await customerGroups.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("CustomerGroupID", "CustomerGroupName");
        }

        public async Task<SelectList> CountryList()
        {
            var objList = await countries.GetList(AppData.GetAPIKey());
            return objList.ToSelectList("CountryName", "CountryName");
        }

    }
}

