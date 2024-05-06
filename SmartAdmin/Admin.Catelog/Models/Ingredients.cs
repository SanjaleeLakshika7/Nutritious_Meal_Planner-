using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{

    public class Ingredients
    {
        #region Data

        [Key]
        [Display(Name = "ID")]
        public string IngredientsID { get; set; }

        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item")]
        public string ItemID { get; set; }

        [Required(ErrorMessage = "Caption is required")]
        [Display(Name = "Caption")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DisOrder is required")]
        [Display(Name = "DisOrder")]
        public int DisOrder { get; set; } = 0;

        #endregion


        #region Supporting

        public string ReturnURL { get; set; } = "/Ingredients/Index";

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }


        #endregion


        #region Display


        [Display(Name = "Item")]
        public string ItemDisplay
        {
            get
            {
                return $"{ItemCode} {ItemName}";
            }
        }



        #endregion
    }

    public class IngredientsSearchView
    {
        public string KeyW { get; set; } = "";
        public string ItemID { get; set; } = "";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public int RecordCount { get; set; } = 0;

        public List<int> PaginationList = new List<int>();
    }

    public class IngredientsEditView
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; } = "";
        public Ingredients Ingredients { get; set; }

        public List<Ingredients> IngredientsList = new List<Ingredients>();

        public string ReturnURL { get; set; } = "/ItemSize/Index";
    }
}
