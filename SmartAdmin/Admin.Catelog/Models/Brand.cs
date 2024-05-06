using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class Brand
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string BrandID { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/Brand/Index";

        #endregion


        #region Display


        #endregion
    }


    public class BrandSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
