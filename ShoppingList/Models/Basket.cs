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
        public int? ShopId { get; set; }
        public virtual Shop? Shop { get; set; }
        public User? User { get; set; }
    }
}
