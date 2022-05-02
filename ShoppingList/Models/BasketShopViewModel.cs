using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingList.Models
{
    public class BasketShopViewModel
    {
        public IList<Basket>? Baskets { get; set; }
        public SelectList? Shops { get; set; }
        public string? ShopName { get; set; }
        public string? SearchString { get; set; }
    }
}
