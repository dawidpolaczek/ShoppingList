﻿using ShoppingList.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketEditViewModel : BasketBasicViewModel
    {
        [Required]
        public int BasketId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public IList<Product>? AvailableProducts { get; set; }
        public IList<Product>? SelectedProducts { get; set; }

        public IList<Shop>? AvailableShops { get; set; }
        public IList<Shop>? SelectedShops { get; set; }
    }
}
