using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Shop
{
    public class ShopDetailsViewModel
    {
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        public int ShopId { get; set; }
    }
}
