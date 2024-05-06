using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Content.Models
{
    public class SettingsCategory
    {
        #region Data

       
        [Display(Name = "Category Code")]
        public string CategoryCode { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DisOrder tag is required")]
        [Display(Name = "DisOrder")]
        public int DisOrder { get; set; } 

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/AppSettings/Settings";

        #endregion


        #region Display


        #endregion
    }


    public class SettingsCategorySearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
