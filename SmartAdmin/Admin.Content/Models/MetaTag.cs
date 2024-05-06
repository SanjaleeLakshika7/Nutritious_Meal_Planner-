using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Content.Models
{
    public class MetaTag
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string PageID { get; set; }

        [Required(ErrorMessage = "Page name is required")]
        [Display(Name = "Page Name")]
        public string PageName { get; set; }

        [Required(ErrorMessage = "Title tag is required")]
        [Display(Name = "Title")]
        public string TitleTag { get; set; } = "";

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string DescriptionTag { get; set; } = "";

        [Display(Name = "Keywords")]
        public string KeywordsTag { get; set; } = "";

        [Display(Name = "OG Title")]
        public string OGTitleTag { get; set; } = "";

        [DataType(DataType.MultilineText)]
        [Display(Name = "OG Description")]
        public string OGDescriptionTag { get; set; } = "";

        [Display(Name = "OG Image URL")]
        public string OGImageTag { get; set; } = "";



        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/MetaTag/Index";

        #endregion


        #region Display


        #endregion
    }


    public class MetaTagSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
