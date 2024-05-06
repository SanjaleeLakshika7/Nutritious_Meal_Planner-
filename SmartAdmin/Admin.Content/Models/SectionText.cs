using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Content.Models
{
    public class SectionText
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string SectionID { get; set; }

        [Required(ErrorMessage = "Section name is required")]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Body text is required")]
        [Display(Name = "Body Text")]
        public string SectionBodyText { get; set; } = "";


        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/SectionText/Index";

        #endregion


        #region Display


        #endregion
    }


    public class SectionTextSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
    public class SectionTextView
    {
        public SectionText Item { get; set; }
        
        public List<SectionText> RelatedList = new List<SectionText>();
    }
}
