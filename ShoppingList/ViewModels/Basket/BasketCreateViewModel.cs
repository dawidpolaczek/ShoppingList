using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Helpers;
using ShoppingList.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketCreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? SpecificDate { get; set; }

        [Display(Name = "Regular shopping day")]
        public DayOfWeek? DayEveryWeek { get; set; }

        [Display(Name = "Day of the week")]
        public SelectList DaysOfWeek { get => SelectListHelpers.GetSelectListOfEnum<DayOfWeek>(); }

        [Display(Name = "Shop")]
        public SelectList? ShopsList { get; set; }
        public int? SelectedShopId { get; set; }

        [Display(Name = "Products")]
        public MultiSelectList? ProductsList { get; set; }
        public IList<int>? SelectedProductsIds { get; set; }
    }
}
