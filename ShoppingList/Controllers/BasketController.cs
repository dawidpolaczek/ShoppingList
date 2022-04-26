using Microsoft.AspNetCore.Mvc;
using ShoppingList.Services;

namespace ShoppingList.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public string Index()
        {
            return "Index";
        }
    }
}
