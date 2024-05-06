using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class ItemSize
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string ItemSizeID { get; set; }

        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item")]
        public string ItemID { get; set; }

        [Required(ErrorMessage = "Size Name is required")]
        [Display(Name = "Size Name ")]
        public string SizeName { get; set; }

        [Required(ErrorMessage = "Price Variation is required")]
        [Display(Name = "Price Variation")]
        public double PriceVariation { get; set; } = 0;

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/ItemSize/Index";

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

       
        #endregion


        #region Display


        [Display(Name = "Item")]
        public string ItemDisplay
        {
            get
            {
                return $"{ItemCode} {ItemName}";
            }
        }



        #endregion
    }

    public class ItemSizeSearchView
    {
        public string KeyW { get; set; } = "";
        public string ItemID { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }

    public class ItemSizeEditView
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; } = "";
        public ItemSize ItemSize { get; set; }

        public List<ItemSize> ItemSizeList = new List<ItemSize>();

        public string ReturnURL { get; set; } = "/ItemSize/Index";
    }
}
