using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Models
{
    public class CatalogSettings
    {
        #region Data


        [Display(Name = "Restrict updating catelogue from SmartAdmin")]
        public string cat_RestrictUpdate { get; set; } = "";

        [Display(Name = "Allow using the same item name for many item")]
        public string cat_ItemNameDuplicateAllow { get; set; } = "";

        [Display(Name = "Default image width")]
        public string cat_ImageSizeWidth { get; set; } = "";

        [Display(Name = "Default Image Height")]
        public string cat_ImageSizeHeight { get; set; } = "";


        #endregion

       
        #region Supporting

        public string ReturnURL { get; set; } = "/AppSettings/Settings";

        #endregion


        #region Display

        [Display(Name = "Restrict updating catelogue from SmartAdmin")]
        public bool cat_RestrictUpdateBool
        {
            get
            {
                return cat_RestrictUpdate == "1";
            }

            set
            {
                this.cat_RestrictUpdate = value ? "1" : "0";
            }
        }

        [Display(Name = "Allow using the same item name for many item")]
        public bool cat_ItemNameDuplicateAllowBool
        {
            get
            {
                return cat_ItemNameDuplicateAllow == "1";
            }

            set
            {
                this.cat_ItemNameDuplicateAllow = value ? "1" : "0";
            }
        }

        #endregion
    }

}
