using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketEditViewModel : BasketBasicViewModel
    {
        [Required]
        public int BasketId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public SelectList? DaysOfWeek { get; set; }
    }
}
