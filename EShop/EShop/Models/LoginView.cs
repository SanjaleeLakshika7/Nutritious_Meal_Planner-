using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;

namespace EShop.Models
{
    public class LoginView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string LoginEmail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [NotMapped] // Does not effect with your database
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Old password is required")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        public string ReturnURL { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = true;

        [Display(Name = "User ID")]
        public string UserID { get; set; } = "";

        [Display(Name = "TokenID")]
        public string TokenID { get; set; } = "";
    }
}
