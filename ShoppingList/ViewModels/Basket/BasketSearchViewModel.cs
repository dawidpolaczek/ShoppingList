using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketSearchViewModel
    {
        public IList<BasketTableViewModel>? Baskets { get; set; }
        public SelectList? Shops { get; set; }
        public string? ShopName { get; set; }
        public string? SearchString { get; set; }
    }
}
