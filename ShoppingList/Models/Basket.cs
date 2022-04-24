using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DayOfWeek? DayOfWeek { get; set; }

        // Navigation properties and/or IDs:
        public virtual ICollection<Product>? Products { get; set; }
        public int? ShopId { get; set; }
        public virtual Shop? Shop { get; set; }
    }
}
