using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DropDown
{
    public interface ISalesDDL
    {
        Task<SelectList> CountryList();
        Task<SelectList> CustomerCategoryList();
        Task<SelectList> CustomerGroupList();
    }
}