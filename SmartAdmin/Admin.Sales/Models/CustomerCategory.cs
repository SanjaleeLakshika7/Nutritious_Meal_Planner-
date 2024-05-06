using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class CustomerCategory
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string CustomerCategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string CustomerCategoryName { get; set; }

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/CustomerCategory/Index";

        #endregion


        #region Display


        #endregion
    }


    public class CustomerCategorySearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
