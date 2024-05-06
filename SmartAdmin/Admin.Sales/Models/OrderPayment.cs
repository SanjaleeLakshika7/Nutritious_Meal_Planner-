using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
   public class OrderPayment
    {
        #region Data

        [Key]
   
        [Display(Name = "PaymentID")]
        public string PaymentID { get; set; }

       
        [Display(Name = "OrderID")]
        public string OrderID { get; set; }

    
        [Display(Name = "TransactionID")]
        public string TransactionID { get; set; }


        [Display(Name = "TransactionType")]
        public string TransactionType { get; set; }

        [Display(Name = "Price")]
        public float ItemPrice { get; set; }

        public string PaymentStatus { get; set; }

        #endregion


        #region Supporting

        //public string ReturnURL { get; set; } = "/Customer/Index";

        #endregion


        #region Display


        #endregion
    }


    public class OrderPaymentSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
