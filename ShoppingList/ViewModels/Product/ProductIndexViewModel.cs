using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public IList<ProductTableViewModel>? Products { get; set; }
        
        [Display(Name = "Product's name")]
        public string? SearchString { get; set; }
    }
}
