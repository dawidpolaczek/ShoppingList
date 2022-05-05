using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketCreateViewModel : BasketBasicViewModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Display(Name = "Day of the week")]
        public SelectList? DaysOfWeek { get; set; }

        public SelectList? Shops { get; set; }
    }
}
