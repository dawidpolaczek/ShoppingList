namespace ShoppingList.Models
{
    public class User
    {
        public int Id { get; set; }

        // Navigation properties and/or IDs:
        public ICollection<Product>? Products { get; set; }
        public ICollection<Basket>? Baskets { get; set; }
        public ICollection<Shop>? Shops { get; set; }
    }
}
