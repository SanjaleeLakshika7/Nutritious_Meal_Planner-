using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;
using Sales.Models;

namespace EShop.Models
{
    public class CheckoutView
    {
        public List<CartItem> CartList = new List<CartItem>();
        public SaleOrder Order { get; set; }
        [Required(ErrorMessage = "Schedule Type is required")]
        [Display(Name = "Schedule Type")]
        public string ScheduleType { get; set; }
}
}
