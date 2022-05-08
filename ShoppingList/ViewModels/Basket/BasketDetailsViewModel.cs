using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketDetailsViewModel
    {
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Date")]
        public string? SpecificDate { get; set; }

        [Display(Name = "Regular shopping day")]
        public string? DayEveryWeek { get; set; }

        public int BasketId { get; set; }
        public string? ShopName { get; set; }
    }
}
