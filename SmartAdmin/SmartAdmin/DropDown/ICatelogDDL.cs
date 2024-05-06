using Content.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DropDown
{
    public interface ICatelogDDL
    {
        Task<SelectList> BrandList();
        Task<SelectList> CategoryMainList();
        Task<SelectList> CategorySubList();
        Task<SelectList> CategorySubList(string CategoryMainID);
        Task<SelectList> ItemTypeList();

        Task<string> GetImageSizeHeight();
        Task<string> GetImageSizeWidth();
    }
}