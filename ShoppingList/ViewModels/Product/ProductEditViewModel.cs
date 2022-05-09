using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Product
{
    public class ProductEditViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(300, MinimumLength = 2)]
        public string? Description { get; set; }
    }
}
