using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Content.Models
{
    public class ContactForm
    {
        #region Data
        
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string SenderEmail { get; set; } = "";

        [Required(ErrorMessage = "Message is Required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid Contact Number")]
        [Required(ErrorMessage = "Contact Number is required")]
        [Display(Name = "Contact Number")]
        public string SenderPhoneNo { get; set; } = "";

        [Required(ErrorMessage = "Subject is required")]
        [Display(Name = "Subject")]
        public string SenderSubject { get; set; } = "";

       



        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "";

        #endregion


        #region Display


        #endregion
    }


    public class ContactFormSearchView
    {
        public string KeyW { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
