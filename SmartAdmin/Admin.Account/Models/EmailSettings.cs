using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Models
{
    public class EmailSettings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email Server is required")]
        [Display(Name = "Email Server / Host")]
        public string EmailServer { get; set; }

        [Required(ErrorMessage = "Sender Display Name is required")]
        [Display(Name = "Sender Display Name")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Website URL is required")]
        [Display(Name = "Website URL")]
        public string WebURL { get; set; }

        [Required(ErrorMessage = "Sender Email Name is required")]
        [Display(Name = "Sender Email Address")]
        public string SenderEmail { get; set; }

        [Display(Name = "Use Authentication")]
        public bool UseAuthentication { get; set; }

        [Display(Name = "Sender Username")]
        public string SenderUsername { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Sender Password")]
        public string SenderPassword { get; set; }

        [Display(Name = "Port Number")]
        public int PortNumber { get; set; }

        [Display(Name = "Use SSL")]
        public bool UseSSL { get; set; }

    }

    
}
