using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Models
{
    public class Customer
    {
        #region Data Personal

        [Key]
        [Display(Name = "ID")]
        public string CustomerID { get; set; }

        [Display(Name = "Facebook")]
        public string FacebookID { get; set; } = "";

        [Display(Name = "Google")]
        public string GoogleID { get; set; } = "";

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string CustomerCategoryID { get; set; } = "0000";

        [Display(Name = "Group")]
        public string CustomerGroupID { get; set; } = "";

        [Display(Name = "Price Type")]
        public string PriceType { get; set; } = "RT";

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string LoginEmail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }

        #endregion

        #region Data Identity

        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public string Salutation { get; set; } = "";

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; } = "";

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; } = "";

        [Required(ErrorMessage = "Address line 1 is required")]
        [Display(Name = "Address Line 1")]
        [DataType(DataType.MultilineText)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [DataType(DataType.MultilineText)]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; } = "Sri Lanka";

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+\d{11}$", ErrorMessage = "Invalid phone number please enter with Country Code ex.+94764671111")]
        [Display(Name = "Mobile Number")]
        public string TelephoneMobile { get; set; } = "";

        [RegularExpression(@"^\+\d{11}$", ErrorMessage = "Invalid phone number please enter with Country Code ex.+94764671111")]
        [Display(Name = "Other Number")]
        public string TelephoneOther { get; set; } = "";

        #endregion

        #region Data Shipping

        [Display(Name = "Different shipping address")]
        public string ShipAway { get; set; } = "N";

        [Required(ErrorMessage = "Att. is required")]
        [Display(Name = "Att. ")]
        public string ShipAttTo { get; set; } = "";

        [Required(ErrorMessage = "Address line 1 is required")]
        [Display(Name = "Address Line 1")]
        public string ShipAddressLine1 { get; set; } = "";

        [Display(Name = "Address Line 2")]
        public string ShipAddressLine2 { get; set; } = "";

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string ShipCity { get; set; } = "";

        [Display(Name = "State")]
        public string ShipState { get; set; } = "";

        [Display(Name = "Postal Code")]
        public string ShipPostalCode { get; set; } = "";

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string ShipCountry { get; set; } = "Sri Lanka";

        #endregion

        #region Data Other

        [Display(Name = "Remarks")]
        public string Remarks { get; set; } = "";

        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string ActiveStatus { get; set; } = "A";
        public int checkLoginData { get; set; } = 0;
        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/Customer/Index";

        [Display(Name = "Category")]
        public string CustomerCategoryName { get; set; }

        [Display(Name = "Group")]
        public string CustomerGroupName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("LoginPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Terms and Condition is required")]
        [Display(Name = "Terms and Condition")]
        public bool CheckTermsandCondition { get; set; } = false;

        public bool UseAuthentication { get; set; }

        #endregion


        #region Display

        [Display(Name = "Customer")]
        public string CustomerDisplay
        {
            get
            {
                return $"{Salutation} {FirstName} {LastName}";
            }
        }

        [Display(Name = "Address")]
        public string AddressDisplay
        {
            get
            {
                var retVal = "";
                retVal += AddressLine1 != "" ? $", {AddressLine1}" : "";
                retVal += AddressLine2 != "" ? $", {AddressLine2}" : "";
                retVal += City != "" ? $", {City}" : "";
                retVal += State != "" ? $", {State}" : "";
                retVal += PostalCode != "" ? $", {PostalCode}" : "";
                retVal += Country != "" ? $", {Country}" : "";

                return retVal.StartsWith(", ") ? retVal.Remove(0, 2) : retVal;
            }
        }

        [Display(Name = "Address")]
        public string AddressShortDisplay
        {
            get
            {
                var retVal = "";
                retVal += City != "" ? $", {City}" : "";
                retVal += State != "" ? $", {State}" : "";
                retVal += Country != "" ? $", {Country}" : "";

                return retVal.StartsWith(", ") ? retVal.Remove(0, 2) : retVal;
            }
        }

        [Display(Name = "Price Type")]
        public string PriceTypeText
        {
            get
            {
                string retVal = "";
                switch (PriceType)
                {
                    case "":
                        retVal = "(Not Set)";
                        break;

                    case "RT":
                        retVal = "Retail";
                        break;

                    case "WH":
                        retVal = "Wholesale";
                        break;

                    default:
                        retVal = PriceType;
                        break;
                }
                return retVal;
            }
        }

        [Display(Name = "Shipping")]
        public bool ShipAwayBool
        {
            get
            {
                return ShipAway == "Y";
            }

            set
            {
                this.ShipAway = value ? "Y" : "N";
            }
        }

        [Display(Name = "Different Shipping Address")]
        public string ShipAwayText
        {
            get
            {
                string retVal = "";
                switch (ShipAway)
                {
                    case "Y":
                        retVal = "Yes";
                        break;

                    case "N":
                        retVal = "-";
                        break;

                    default:
                        retVal = ShipAway;
                        break;
                }
                return retVal;
            }
        }

        [Display(Name = "Registered On")]
        public string CreatedDateDisplay
        {
            get
            {
                return CreatedDate.ToShortDateString();
            }
        }

        [Display(Name = "Status")]
        public bool ActiveStatusBool
        {
            get
            {
                return ActiveStatus == "A";
            }

            set
            {
                this.ActiveStatus = value ? "A" : "I";
            }
        }

        [Display(Name = "Status")]
        public string ActiveStatusText
        {
            get
            {
                string retVal = "";
                switch (ActiveStatus)
                {
                    case "A":
                        retVal = "Active";
                        break;

                    case "I":
                        retVal = "Inactive";
                        break;

                    default:
                        retVal = ActiveStatus;
                        break;
                }
                return retVal;
            }
        }

        public string ActiveStatusClass
        {
            get
            {
                string retVal = "";
                switch (ActiveStatus)
                {
                    case "A":
                        retVal = "success";
                        break;

                    case "I":
                        retVal = "danger";
                        break;

                    default:
                        retVal = "";
                        break;
                }
                return retVal;
            }
        }

        #endregion
    }


    public class CustomerSearchView
    {
        public string KeyW { get; set; } = "";
        public string CustomerCategoryID { get; set; } = "";
        public string CustomerGroupID { get; set; } = "";
        public string PriceType { get; set; } = "";
        public string ActiveStatus { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }
}
