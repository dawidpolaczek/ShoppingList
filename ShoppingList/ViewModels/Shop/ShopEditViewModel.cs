using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Shop
{
    public class ShopEditViewModel
    {
        [Required]
        public int ShopId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 2)]
        public string? Address { get; set; }
    }
}
