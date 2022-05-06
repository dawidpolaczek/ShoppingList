using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public class EntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
