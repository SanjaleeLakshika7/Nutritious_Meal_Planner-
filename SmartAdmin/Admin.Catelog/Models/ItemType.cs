using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class ItemType
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string ItemTypeID { get; set; }

        [Required(ErrorMessage = "Item type name is required")]
        [Display(Name = "Item Type Name")]
        public string ItemTypeName { get; set; }

        [Display(Name = "Display Order")]
        public int DisOrder { get; set; } = 0; 

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/ItemType/Index";

        #endregion


        #region Display


        #endregion
    }


    public class ItemTypeSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
