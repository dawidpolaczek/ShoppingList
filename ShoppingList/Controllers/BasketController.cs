using Microsoft.AspNetCore.Mvc;
using ShoppingList.Services;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
