using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class CustomerGroup
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string CustomerGroupID { get; set; }

        [Required(ErrorMessage = "Group name is required")]
        [Display(Name = "Group Name")]
        public string CustomerGroupName { get; set; }

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/CustomerGroup/Index";

        #endregion


        #region Display


        #endregion
    }


    public class CustomerGroupSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
