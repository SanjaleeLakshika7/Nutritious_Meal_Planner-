using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Models
{
    public class SaleOrder
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string OrderID { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer")]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Att. is required")]
        [Display(Name = "Att. ")]
        public string ShipAttTo { get; set; } = "";

        [Required(ErrorMessage = "Address line 1 is required")]
        [Display(Name = "Address Line 1")]
        public string ShipAddressLine1 { get; set; } = "";

        [Display(Name = "Address Line 2")]
        public string ShipAddressLine2 { get; set; } = "";

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string ShipCity { get; set; } = "";

        [Display(Name = "State")]
        public string ShipState { get; set; } = "";

        [Display(Name = "Postal Code")]
        public string ShipPostalCode { get; set; } = "";

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string ShipCountry { get; set; } = "Sri Lanka";

        [Display(Name = "Remarks")]
        public string Remarks { get; set; } = "";

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string OrderStatus { get; set; } = "P";

        [Required(ErrorMessage = "Paid Status is required")]
        [Display(Name = "Status")]
        public string PaidStatus { get; set; } = "NP";

        [Display(Name = "Next Recurring Date")]
        public DateTime NextRecurringDate { get; set; }

        [Display(Name = "Schedule Type")]
        public string PeriodIndex { get; set; } = "";
        [Display(Name = "Stop Recurring")]
        public string StopRecurringStatus { get; set; } = "";

        [Display(Name = "Sheduled Status")]

        public string RecurringSheduledStatus { get; set; } = "";
        [Display(Name = "Sheduled Time")]
        public string SheduledTime { get; set; } = "";
        
        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/SaleOrder/Index";

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }


        [Display(Name = "Mobile Number")]
        public string TelephoneMobile { get; set; } = "";

        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Display(Name = "Items")]
        public int ItemCount { get; set; }

        [Display(Name = "Total")]
        public double ItemTotal { get; set; }


        #endregion


        #region Display

        [Display(Name = "Date")]
        public string OrderDateDisplay
        {
            get
            {
                return OrderDate.ToShortDateString();
            }
        }

        [Display(Name = "Date")]
        public string OrderDateTimeDisplay
        {
            get
            {
                return OrderDate.ToString();
            }
        }

        [Display(Name = "Address")]
        public string AddressDisplay
        {
            get
            {
                var retVal = "";
                retVal += ShipAddressLine1 != "" ? $", {ShipAddressLine1}" : "";
                retVal += ShipAddressLine2 != "" ? $", {ShipAddressLine2}" : "";
                retVal += ShipCity != "" ? $", {ShipCity}" : "";
                retVal += ShipState != "" ? $", {ShipState}" : "";
                retVal += ShipPostalCode != "" ? $", {ShipPostalCode}" : "";
                retVal += ShipCountry != "" ? $", {ShipCountry}" : "";

                return retVal.StartsWith(", ") ? retVal.Remove(0, 2) : retVal;
            }
        }

        [Display(Name = "Address")]
        public string AddressShortDisplay
        {
            get
            {
                var retVal = "";
                retVal += ShipCity != "" ? $", {ShipCity}" : "";
                retVal += ShipState != "" ? $", {ShipState}" : "";
                retVal += ShipCountry != "" ? $", {ShipCountry}" : "";

                return retVal.StartsWith(", ") ? retVal.Remove(0, 2) : retVal;
            }
        }

        [Display(Name = "Total")]
        public string ItemTotalDisplay
        {
            get
            {
                return $"{ItemTotal.ToString("N")} ({ItemCount})";
            }
        }

        [Display(Name = "Status")]
        public bool OrderStatusBool
        {
            get
            {
                return OrderStatus == "A";
            }

            set
            {
                this.OrderStatus = value ? "A" : "I";
            }
        }

        [Display(Name = "Status")]
        public string OrderStatusText
        {
            get
            {
                string retVal = "";
                switch (OrderStatus)
                {
                    case "P":
                        retVal = "Pending";
                        break;
                    case "A":
                        retVal = "In Progress";
                        break;
                    case "D":
                        retVal = "On Delivery";
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

        public string PeriodIndexText
        {
            get
            {
                string retVal = "";
                switch (PeriodIndex)
                {
                    case "O":
                        retVal = "One Time";
                        break;
                    case "D":
                        retVal = "Dialy";
                        break;
                     
                }
                return retVal;
            }
        }

        public string OrderStatusClass
        {
            get
            {
                string retVal = "";
                switch (OrderStatus)
                {
                    case "P":
                        retVal = "warning";
                        break;
                    case "A":
                        retVal = "primary";
                        break;
                    case "D":
                        retVal = "info";
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
                        retVal = "default";
                        break;

                }
                return retVal;
            }
        }

        [Display(Name = "Status")]
        public bool PaidStatusBool
        {
            get
            {
                return PaidStatus == "PD";
            }

            set
            {
                this.OrderStatus = value ? "PD" : "NP";
            }
        }

        [Display(Name = "Paid Status")]
        public string PaidStatusText
        {
            get
            {
                string retVal = "";
                switch (PaidStatus)
                {
                    case "PD":
                        retVal = "Paid";
                        break;
                    case "NP":
                        retVal = "Not Paid";
                        break;
                    

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        public string PaidStatusClass
        {
            get
            {
                string retVal = "";
                switch (PaidStatus)
                {
                    case "PD":
                        retVal = "success";
                        break;
                    case "NP":
                        retVal = "danger";
                        break;

                    default:
                        retVal = "default";
                        break;

                }
                return retVal;
            }
        }

        #endregion
    }


    public class SaleOrderSearchView
    {
        public string KeyW { get; set; } = "";
        public string CustomerID { get; set; } = "";
        public string OrderStatus { get; set; } = "";

        public string PaidStatus { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }

    public class SaleOrderDetailsView
    {
        public SaleOrder Order { get; set; }
        public Customer Customer { get; set; }

        public List<OrderItem> ItemList = new List<OrderItem>();
        public List<OrderAction> ActionList = new List<OrderAction>();

        [Required(ErrorMessage = "Remark is required")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Action type is required")]
        [Display(Name = "Action Type")]
        public string ActionType { get; set; }

        public string OrderID { get; set; } = "";
        public string ReturnURL { get; set; } = "/SaleOrder/Index";
    }
}
