using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketBasicViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Day of the week")]
        public DayOfWeek? DayEveryWeek { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? SpecificDate { get; set; }
    }
}
