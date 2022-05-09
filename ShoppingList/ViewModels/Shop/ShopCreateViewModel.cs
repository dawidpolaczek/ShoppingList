using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Shop
{
    public class ShopCreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 2)]
        public string? Address { get; set; }
    }
}
