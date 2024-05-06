using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Models
{
    public class APIKey
    {
        #region Data

        [Key]
        [Display(Name = "Key ID")]
        public string KeyID { get; set; }

        //[Required(ErrorMessage = "Country name is required")]
        [Display(Name = "Key Value")]
        public string KeyValue { get; set; }

        [Display(Name = "Key Details")]
        public string KeyDetails { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Active Status")]
        public string ActiveStatus { get; set; } = "A";

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/KeySettings/Index";

        #endregion


        #region Display

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

        #endregion
    }

    public class APIKeySearchView
    {
        public string KeyW { get; set; } = "";
        public string ActiveStatus { get; set; } = "";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
