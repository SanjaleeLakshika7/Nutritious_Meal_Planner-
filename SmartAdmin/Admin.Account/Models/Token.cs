using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Models
{
    public class Token
    {
        [Key]
        [Display(Name = "ID")]
        public string TokenID { get; set; } = "";

        [Display(Name = "Type")]
        public string TokenType { get; set; } = "";

        [Display(Name = "Reference")]
        public string RefID { get; set; } = "";

        [Display(Name = "Data")]
        public string TokenData { get; set; } = "";

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public string TokenStatus { get; set; } = "A";


        // Supporting 

        public bool IsValid
        {
            get
            {
                bool retValue = false;
                if (TokenStatus == "A") retValue = true;

                if (DateTime.Now > ExpiryDate) retValue = false;

                return retValue;
            }
        }

        public string TokenStatusText
        {
            get
            {
                string retValue = "Active";
                if (TokenStatus == "A") retValue = "active";
                if (TokenStatus == "SS") retValue = "not valid";
                if (TokenStatus == "CM") retValue = "already used";

                if (TokenStatus == "A" && DateTime.Now > ExpiryDate) retValue = "expired";

                return retValue;
            }
        }

    }

   
}
