using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class CategoryMain
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string CategoryMainID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string CategoryMainName { get; set; }

        [Display(Name = "Default Session")]
        public int DefaultSession { get; set; } = 0;

        [Display(Name = "Service Charges")]
        public int ServiceCharge { get; set; } = 0;

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/CategoryMain/Index";

        #endregion


        #region Display

        [Display(Name = "Service Charges")]
        public bool ServiceChargeBool
        {
            get
            {
                return ServiceCharge == 1;
            }

            set
            {
                this.ServiceCharge = value ? 1 : 0;
            }
        }


        #endregion
    }


    public class CategoryMainSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
