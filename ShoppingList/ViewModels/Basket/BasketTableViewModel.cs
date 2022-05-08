using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketTableViewModel
    {
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Next shopping date")]
        [DataType(DataType.Date)]
        public DateTime? NextShoppingDate { get; set; }

        public int BasketId { get; set; }

        [Display(Name = "Amount of products")]
        public int Size { get; set; }
    }
}
