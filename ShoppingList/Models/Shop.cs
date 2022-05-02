﻿namespace ShoppingList.Models
{
    public class Shop : EntityBase
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Image { get; set; }

        // Navigation properties and/or IDs:
        public virtual ICollection<Basket>? Baskets { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
