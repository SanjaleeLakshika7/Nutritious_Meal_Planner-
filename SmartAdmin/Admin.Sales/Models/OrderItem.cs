using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Models
{
    public class OrderItem
    {
        #region Data

        [Key]
        [Display(Name = "Item ID")]
        public string OrderItemID { get; set; }

        [Display(Name = "ID")]
        public string OrderID { get; set; }

        [Display(Name = "Item ID")]
        public string ItemID { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; } = "";

        

        [Display(Name = "Item Price")]
        public double ItemPrice { get; set; }

        [Display(Name = "Discount")]
        public double ItemDiscount { get; set; }

        [Display(Name = "Item Qty")]
        public double ItemQty { get; set; }

        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Transaction")]
        public string Transaction { get; set; }

        
        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/OrderItem/Index";

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Sub Total")]
        public double LineTotal
        {
            get
            {
                return (ItemPrice - ItemDiscount) * ItemQty;
            }
        }

        #endregion


        #region Display

        [Display(Name = "Price")]
        public string ItemPriceDisplay
        {
            get
            {
                return ItemPrice.ToString("N");
            }
        }

        [Display(Name = "Discount")]
        public string ItemDiscountDisplay
        {
            get
            {
                return ItemDiscount > 0 ? ItemDiscount.ToString("N") : "-";
            }
        }
        

        [Display(Name = "Remarks")]
        public string RemarksDisplay
        {
            get
            {
                return Remarks != null ? Remarks.ToString() : "-";
            }
        }

        [Display(Name = "Sub Total")]
        public string LineTotalDisplay
        {
            get
            {
                return LineTotal.ToString("N");
            }
        }


        #endregion
    }


    public class OrderItemSearchView
    {
        public string KeyW { get; set; } = "";
        public string OrderID { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
