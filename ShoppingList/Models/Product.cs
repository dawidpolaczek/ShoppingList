namespace ShoppingList.Models
{
    public class Product : EntityBase
    {
        public string? Description { get; set; }

        // Navigation properties and/or IDs:
        public virtual ICollection<Basket>? Baskets { get; set; }
        public User? User { get; set; }
    }
}
