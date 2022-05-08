﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Product
{
    public class ProductCreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(300, MinimumLength = 2)]
        public string? Description { get; set; }
    }
}
