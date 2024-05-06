using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class Country
    {
        #region Data

        [Key]
        [Required(ErrorMessage = "Code name is required")]
        [Display(Name = "Code")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/Customer/Index";

        #endregion


        #region Display


        #endregion
    }


    public class CountrySearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
