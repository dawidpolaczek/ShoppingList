using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingList.Models
{
    public class BasketShopViewModel
    {
        public IList<Basket>? Baskets { get; set; }
        public SelectList? Shops { get; set; }
        public string? BasketShopName { get; set; }
        public string? Name { get; set; }
    }
}
