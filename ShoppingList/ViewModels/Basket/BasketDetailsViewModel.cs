using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ViewModels.Basket
{
    public class BasketDetailsViewModel
    {
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Next shopping date")]
        [DataType(DataType.Date)]
        public DateTime? NextShoppingDate { get; set; }
        
        [Display(Name = "Next shopping date")]
        public string NextShoppingDateStr
        {
            get
            {
                if (NextShoppingDate == null)
                    return "undefined or outdated";

                return NextShoppingDate.Value.ToString("dd-MM-yyyy");
            }
        }

        [Display(Name = "Regular shopping day")]
        public string? DayEveryWeek { get; set; }

        public int BasketId { get; set; }

        [Display(Name = "Shop")]
        public Tuple<int, string>? Shop { get; set; }

        [Display(Name = "Products")]
        public IEnumerable<Tuple<int, string>>? Products { get; set; }
    }
}
