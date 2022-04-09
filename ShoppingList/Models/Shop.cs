namespace ShoppingList.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Image { get; set; }

        // Navigation properties and/or IDs:
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Basket>? Baskets { get; set; }
    }
}
