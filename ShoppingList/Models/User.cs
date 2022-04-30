using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Basket>? Baskets { get; set; }
    }
}
