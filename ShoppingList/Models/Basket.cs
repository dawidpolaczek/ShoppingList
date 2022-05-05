using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public class Basket : EntityBase
    {
        [Required]
        public string Name { get; set; } = null!;
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
