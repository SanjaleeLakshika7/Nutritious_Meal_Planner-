using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class DiscountSchema
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string DiscountSchemaID { get; set; }

        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item")]
        public string ItemID { get; set; }

        [Required(ErrorMessage = "Minimum qunatity is required")]
        [Display(Name = "Min. Qty")]
        public double? MinimumQty { get; set; }

        [Display(Name = "Dis. $")]
        public double? DiscountAmount { get; set; }

        [Display(Name = "Dis. %")]
        public double? DiscountRate { get; set; }

        [Display(Name = "Free Qty")]
        public double? FreeQty { get; set; }

        [Display(Name = "Grades")]
        public string GradeList { get; set; }

        [Display(Name = "Price Types")]
        public string PriceTypeList { get; set; } = "RT";

        [Display(Name = "Branches")]
        public string BranchIDList { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string ActiveStatus { get; set; } = "A";

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/DiscountSchema/Index";

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Branches")]
        public string BranchNameList { get; set; } = "(All branches)";

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

        [Display(Name = "Dis. $")]
        public string DiscountAmountDisplay
        {
            get
            {
                return DiscountAmount != null ? DiscountAmount.Value.ToString("N") : "";
            }
        }

        [Display(Name = "Dis. %")]
        public string DiscountRateDisplay
        {
            get
            {
                return DiscountRate != null ? $"{DiscountRate.Value}%" : "";
            }
        }

        [Display(Name = "Free Qty")]
        public string FreeQtyDisplay
        {
            get
            {
                return FreeQty != null ? $"Free {FreeQty.Value}" : "";
            }
        }

        [Display(Name = "Discount")]
        public string DiscountDisplay
        {
            get
            {
                return $"{DiscountAmountDisplay} {DiscountRateDisplay} {FreeQtyDisplay}";
            }
        }

        [Display(Name = "Price Type")]
        public string PriceTypeDisplay
        {
            get
            {
                string retVal = "";
                switch (PriceTypeList)
                {
                    case "RT":
                        retVal = "Retail";
                        break;

                    case "WH":
                        retVal = "Wholesale";
                        break;

                    case "RT,WH":
                        retVal = "(Both)";
                        break;

                    default:
                        retVal = PriceTypeList;
                        break;
                }
                return retVal;
            }
        }

        [Display(Name = "Branches")]
        public string BranchDisplay
        {
            get
            {
                return BranchNameList != "" ? BranchNameList : "(All branches)";
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
                        retVal = "success";
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


    public class DiscountSchemaSearchView
    {
        public string KeyW { get; set; } = "";
        public string ItemID { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }

    public class DiscountSchemaEditView
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; } = "";
        public DiscountSchema DiscountSchema { get; set; }

        public List<DiscountSchema> DiscountSchemaList = new List<DiscountSchema>();

        public string ReturnURL { get; set; } = "/DiscountSchema/Index";
    }
}
