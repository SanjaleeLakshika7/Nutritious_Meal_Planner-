
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Catalog.Models
{
    public class Item
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string ItemID { get; set; }

        [Display(Name = "Cache")]
        public string CacheID { get; set; }

        [Required(ErrorMessage = "Item type is required")]
        [Display(Name = "Item Type")]
        public string ItemTypeID { get; set; }

        [Required(ErrorMessage = "Main category is required")]
        [Display(Name = "Main Category")]
        public string CategoryMainID { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Sub Category")]
        public string CategorySubID { get; set; }

        [Required(ErrorMessage = "Item code is required")]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Retail price is required")]
        [Display(Name = "Retail Price")]
        public double? RetailPrice { get; set; }

        [Required(ErrorMessage = "Wholesale price is required")]
        [Display(Name = "Wholesale Price")]
        public double? WholesalePrice { get; set; }

        [Required(ErrorMessage = "Item cost is required")]
        [Display(Name = "Item Cost")]
        public double? ItemCost { get; set; }

        [Display(Name = "Brand")]
        public string BrandID { get; set; }

        [Display(Name = "Description")]
        public string itemDescription { get; set; }

        [Display(Name = "Thumbnail Image")]
        public IFormFile FileUpload { get; set; }

        [Display(Name = "Stock")]
        public string StockAvailable { get; set; } = "A";

        [Display(Name = "New")]
        public string IsNew { get; set; } = "N";

        [Display(Name = "Best Selling")]
        public string IsFeatured { get; set; } = "N";

        public string IsSpecial { get; set; } = "N";

        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified On")]
        public DateTime LastModifiedDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string ActiveStatus { get; set; } = "A";

        #endregion


        #region Support

        public string ReturnURL { get; set; } = "/Item/Index";

        [Display(Name = "Item Type")]
        public string ItemTypeName { get; set; }

        [Display(Name = "Main Category")]
        public string CategoryMainName { get; set; }

        [Display(Name = "Sub Category")]
        public string CategorySubName { get; set; }

        [Display(Name = "Brand")]
        public string BrandName { get; set; } = "(No brand)";

        [Display(Name = "Price")]
        public double SellingPrice { get; set; } = 0;

        public List<ImageUpload> ImageList = new List<ImageUpload>();

        [Display(Name = "Default image width")]
        public string cat_ImageSizeWidth { get; set; } = "";

        [Display(Name = "Default Image Height")]
        public string cat_ImageSizeHeight { get; set; } = "";


        #endregion


        #region Display

        [Display(Name = "Item")]
        public string ItemDisplay
        {
            get
            {
                return $"{ItemCode} - {ItemName}";
            }
        }

        [Display(Name = "Category")]
        public string CategoryDisplay
        {
            get
            {
                return $"{CategoryMainName} / {CategorySubName}";
            }
        }

        [Display(Name = "Retail")]
        public string RetailDisplay
        {
            get
            {

                return RetailPrice.Value.ToString("N");
            }
        }

        [Display(Name = "Wholesale")]
        public string WholesaleDisplay
        {
            get
            {

                return WholesalePrice.Value.ToString("N");
            }
        }

        [Display(Name = "Price")]
        public string SellingPriceDisplay
        {
            get
            {
                return SellingPrice.ToString("N");
            }
        }

        [Display(Name = "Cost")]
        public string CostDisplay
        {
            get
            {

                return ItemCost.Value.ToString("N");
            }
        }

        [Display(Name = "Brand")]
        public string BrandDisplay
        {
            get
            {
                return BrandName == "" ? "(No brand)" : BrandName;
            }
        }

        [Display(Name = "Stock")]
        public bool StockAvailableBool
        {
            get
            {
                return StockAvailable == "A";
            }

            set
            {
                this.StockAvailable = value ? "A" : "N";
            }
        }

        [Display(Name = "Stock")]
        public string StockAvailableText
        {
            get
            {
                string retVal = "";
                switch (StockAvailable)
                {
                    case "A":
                        retVal = "Available";
                        break;

                    case "N":
                        retVal = "Out of stock";
                        break;

                    default:
                        retVal = StockAvailable;
                        break;
                }
                return retVal;
            }
        }

        public string StockAvailableClass
        {
            get
            {
                string retVal = "";
                switch (StockAvailable)
                {
                    case "A":
                        retVal = "success";
                        break;

                    case "N":
                        retVal = "warning";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        public string IsStockAvailableShowLabelClass
        {
            get
            {
                string retVal = "";
                switch (StockAvailable)
                {
                    case "A":
                        retVal = "d-none";
                        break;

                    case "N":
                        retVal = "";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        public string IsStockAvailableHideLabelClass
        {
            get
            {
                string retVal = "";
                switch (StockAvailable)
                {
                    case "A":
                        retVal = "";
                        break;

                    case "N":
                        retVal = "d-none";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        public string IsStockAvailableShowButton
        {
            get
            {
                string retVal = "";
                switch (StockAvailable)
                {
                    case "A":
                        retVal = "";
                        break;

                    case "N":
                        retVal = "d-none";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        [Display(Name = "New")]
        public bool IsNewBool
        {
            get
            {
                return IsNew == "Y";
            }

            set
            {
                this.IsNew = value ? "Y" : "N";
            }
        }

        [Display(Name = "New")]
        public string IsNewText
        {
            get
            {
                string retVal = "";
                switch (IsNew)
                {
                    case "Y":
                        retVal = "New";
                        break;

                    case "N":
                        retVal = "-";
                        break;

                    default:
                        retVal = IsNew;
                        break;
                }
                return retVal;
            }
        }

        public string IsNewClass
        {
            get
            {
                string retVal = "";
                switch (IsNew)
                {
                    case "Y":
                        retVal = "";
                        break;

                    case "N":
                        retVal = "d-none";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        [Display(Name = "Featured")]
        public bool IsFeaturedBool
        {
            get
            {
                return IsFeatured == "Y";
            }

            set
            {
                this.IsFeatured = value ? "Y" : "N";
            }
        }

        [Display(Name = "Featured")]
        public string IsFeaturedText
        {
            get
            {
                string retVal = "";
                switch (IsFeatured)
                {
                    case "Y":
                        retVal = "Featured";
                        break;

                    case "N":
                        retVal = "-";
                        break;

                    default:
                        retVal = IsFeatured;
                        break;
                }
                return retVal;
            }
        }

        public string IsFeaturedClass
        {
            get
            {
                string retVal = "";
                switch (IsFeatured)
                {
                    case "Y":
                        retVal = "";
                        break;

                    case "N":
                        retVal = "d-none";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }


        [Display(Name = "Special")]
        public bool IsSpecialBool
        {
            get
            {
                return IsSpecial == "Y";
            }

            set
            {
                this.IsSpecial = value ? "Y" : "N";
            }
        }

        [Display(Name = "Special")]
        public string IsSpecialText
        {
            get
            {
                string retVal = "";
                switch (IsSpecial)
                {
                    case "Y":
                        retVal = "Featured";
                        break;

                    case "N":
                        retVal = "-";
                        break;

                    default:
                        retVal = IsSpecial;
                        break;
                }
                return retVal;
            }
        }

        public string IsSpecialClass
        {
            get
            {
                string retVal = "";
                switch (IsSpecial)
                {
                    case "Y":
                        retVal = "";
                        break;

                    case "N":
                        retVal = "d-none";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }


        [Display(Name = "Status")]
        public bool ActiveStatusBool
        {
            get
            {
                return ActiveStatus == "A";
            }

            set
            {
                this.ActiveStatus = value ? "A" : "I";
            }
        }

        [Display(Name = "Status")]
        public string ActiveStatusText
        {
            get
            {
                string retVal = "";
                switch (ActiveStatus)
                {
                    case "A":
                        retVal = "Active";
                        break;

                    case "I":
                        retVal = "Inactive";
                        break;

                    default:
                        retVal = ActiveStatus;
                        break;
                }
                return retVal;
            }
        }

        public string ActiveStatusClass
        {
            get
            {
                string retVal = "";
                switch (ActiveStatus)
                {
                    case "A":
                        retVal = "primary";
                        break;

                    case "I":
                        retVal = "danger";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        #endregion
    }

    public class ItemSearchView
    {
        public string KeyW { get; set; } = "";
        public string CategoryMainID { get; set; } = "";
        public string CategorySubID { get; set; } = "";
        public string BrandID { get; set; } = "";
        public string StockAvailable { get; set; } = "";
        public string IsNew { get; set; } = "";
        public string IsFeatured { get; set; } = "";
        public string ActiveStatus { get; set; } = "";

        public string SortOrder { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

       

        public List<Item> DataList = new List<Item>();
        public List<int> PaginationList = new List<int>();
    }

    public class ItemEditView
    {
        public string ReturnURL { get; set; } = "/Item/Index";

        public Item Basic { get; set; }

    }

    public class ItemDetailsView
    {
        public Item Item { get; set; }
        public List<ImageUpload> ImageList = new List<ImageUpload>();
        public List<Item> RelatedList = new List<Item>();
    }
}
