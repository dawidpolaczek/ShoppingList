using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Shop
{
    public class ShopIndexViewModel
    {
        public IList<ShopTableViewModel>? Shops { get; set; }

        [Display(Name = "Shop's name")]
        public string? SearchString { get; set; }
    }
}
