using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Models
{
    public class User : IdentityUser
    {
        public ICollection<Basket>? Baskets { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Shop>? Shops { get; set; }
    }
}
