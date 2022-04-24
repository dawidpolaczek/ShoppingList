using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Basket>? Baskets { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Shop>? Shops { get; set; }
    }
}
