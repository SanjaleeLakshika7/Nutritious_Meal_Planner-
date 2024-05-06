using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Content.Models
{
    public class SlideBanner
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string SlideBannerID { get; set; }

         //[Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";

        [Display(Name = "Button Name")]
        public string ButtonName { get; set; } = "";

        [Display(Name = "Button Link")]
        public string ButtonLink { get; set; } = "";

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string ActiveStatus { get; set; } = "A";

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/SlideBanner/Index";

        #endregion


        #region Display


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

        [Display(Name = "Status")]
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


    public class SlideBannerSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
