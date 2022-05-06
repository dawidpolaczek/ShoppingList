using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public class Basket : EntityBase
    {
        public DayOfWeek? DayEveryWeek { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SpecificDate { get; set; }

        // Navigation properties and/or IDs:
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Shop>? Shops { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
