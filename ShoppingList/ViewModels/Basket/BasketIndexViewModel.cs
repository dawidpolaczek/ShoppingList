using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketIndexViewModel
    {
        public IList<BasketTableViewModel>? Baskets { get; set; }
        public SelectList? Shops { get; set; }

        [Display(Name = "Shop")]
        public string? ShopName { get; set; }

        [Display(Name = "Basket's name")]
        public string? SearchString { get; set; }
    }
}
