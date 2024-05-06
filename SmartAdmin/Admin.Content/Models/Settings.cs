using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Models
{
    public class Settings
    {
        #region Data


        [Display(Name = "Setting Name")]
        public string SettingName { get; set; } = "";

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = "";

        [Required(ErrorMessage = "Setting Value is required")]
        [Display(Name = "Setting Value")]
        public string SettingValue { get; set; } = "";

        [Display(Name = "CategoryCode")]
        public string CategoryCode { get; set; } = "";

       
        [Display(Name = "DisOrder")]
        public string DisOrder { get; set; } = "";
        #endregion

        [Display(Name = "Last Updated")]
        public DateTime UpdatedDate { get; set; }

        #region Supporting

        public string ReturnURL { get; set; } = "/AppSettings/Settings";

        public static implicit operator string(Settings v)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Display


        #endregion
    }

    public class SettingsSearchView
    {
        public string KeyW { get; set; } = "";
        public string CategoryCode { get; set; } = "";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
