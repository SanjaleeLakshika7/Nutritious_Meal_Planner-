using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;

namespace EShop.Models
{
    public class CartItem
    {
        public Item Item { get; set; }
        public double ItemPrice { get; set; } = 0;
        public double ItemDiscount { get; set; } = 0;
        public double ItemQty { get; set; } = 1;
        public string Remarks { get; set; } = "";

        #region Support

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

        [Display(Name = "Item Price")]
        public string ItemPriceDisplay
        {
            get
            {
                return ItemPrice.ToString("N");
            }
        }

        [Display(Name = "Item Discount")]
        public string ItemDiscountDisplay
        {
            get
            {
                return ItemDiscount > 0 ? ItemDiscount.ToString("N") : "-";
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
}
