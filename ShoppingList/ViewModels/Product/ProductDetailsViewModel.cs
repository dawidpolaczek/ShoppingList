﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public int ProductId { get; set; }
    }
}
