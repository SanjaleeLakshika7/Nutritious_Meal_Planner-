using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Models
{
    public class GeneralSettings
    {
        #region Data


        [Display(Name = "Shop URL")]
        public string gen_EShopURL { get; set; } = "";

        [Display(Name = "Admin URL")]
        public string gen_SmartAdminURL { get; set; } = "";


        public string ReturnURL { get; set; } = "/AppSettings/Settings";

        #endregion

    }

}
