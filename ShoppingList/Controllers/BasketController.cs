using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using ShoppingList.Services;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class BasketController : Controller
    {

        private readonly IDataService<Basket> _basketService;

        public BasketController(IDataService<Basket> basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
