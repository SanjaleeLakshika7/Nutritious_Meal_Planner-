using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Models
{
    public class OrderAction
    {
        #region Data

        [Key]
        [Display(Name = "Item ID")]
        public string OrderActionID { get; set; }

        [Display(Name = "ID")]
        public string OrderID { get; set; }

        [Display(Name = "Action Date")]
        public DateTime ActionDate { get; set; }

        [Display(Name = "Action Type")]
        public string ActionType { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }


        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/OrderAction/Index";

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        #endregion


        #region Display

        [Display(Name = "Action Date")]
        public string ActionDateDisplay
        {
            get
            {
                return ActionDate.ToShortDateString();
            }
        }

        [Display(Name = "Order Date")]
        public string OrderDateDisplay
        {
            get
            {
                return OrderDate.ToShortDateString();
            }
        }

        [Display(Name = "Action Type")]
        public string ActionTypeText
        {
            get
            {
                string retVal = "";
                switch (ActionType)
                {
                    case "P":
                        retVal = "Ordered";
                        break;
                    case "A":
                        retVal = "Accepted";
                        break;
                    case "D":
                        retVal = "Delivered";
                        break;
                    case "C":
                        retVal = "Completed";
                        break;
                    case "R":
                        retVal = "Rejected";
                        break;
                    case "N":
                        retVal = "Cancelled";
                        break;

                    default:
                        retVal = "Unknown";
                        break;
                }
                return retVal;
            }
        }

        public string ActionTypeClass
        {
            get
            {
                string retVal = "";
                switch (ActionType)
                {
                    case "P":
                        retVal = "warning";
                        break;
                    case "A":
                        retVal = "primary";
                        break;
                    case "D":
                        retVal = "warning";
                        break;
                    case "C":
                        retVal = "success";
                        break;
                    case "R":
                        retVal = "danger";
                        break;
                    case "N":
                        retVal = "dark";
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


    public class OrderActionSearchView
    {
        public string KeyW { get; set; } = "";
        public string OrderID { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
