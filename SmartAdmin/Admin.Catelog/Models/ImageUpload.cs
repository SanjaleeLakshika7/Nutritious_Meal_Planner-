using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class ImageUpload
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string ImageID { get; set; }

        [Required(ErrorMessage = "Related key name is required")]
        [Display(Name = "Related Key")]
        public string RefID { get; set; }
        #endregion


        #region Supporting

        [Display(Name = "Uploaded On")]
        public DateTime? UploadDate { get; set; }

        #endregion


        #region Display

        [Display(Name = "Uploaded On")]
        public string UploadDateDisplay
        {
            get
            {
                return UploadDate.HasValue ? UploadDate.Value.ToShortDateString() : "";
            }
        }


        #endregion
    }


   
}
