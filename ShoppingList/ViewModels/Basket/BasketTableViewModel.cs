using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketTableViewModel : BasketBasicViewModel
    {
        public int BasketId { get; set; }

        [Display(Name = "Amount of products")]
        public int Size { get; set; }
    }
}
