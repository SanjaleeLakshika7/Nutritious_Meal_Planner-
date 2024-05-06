using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class CategorySub
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string CategorySubID { get; set; }

        [Required(ErrorMessage = "Sub category name is required")]
        [Display(Name = "Sub Category Name")]
        public string CategorySubName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string CategoryMainID { get; set; }

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/CategoryMain/Index";

        [Display(Name = "Category")]
        public string CategoryMainName { get; set; }

        #endregion


        #region Display

        [Display(Name = "Category")]
        public string CategoryDisplay
        {
            get
            {
                return $"{CategoryMainName} / {CategorySubName}";
            }
        }


        #endregion
    }


    public class CategorySubSearchView
    {
        public string KeyW { get; set; } = "";
        public string CategoryMainID { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
