using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using ShoppingList.Services;
using ShoppingList.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingList.Controllers
{
    public class BasketsController : Controller
    {

        private readonly IDataService<Basket> _basketService;
        private readonly ICurrentUserService _currentUser;

        public BasketsController(IDataService<Basket> basketService, ICurrentUserService currentUserService)
        {
            _basketService = basketService;
            _currentUser = currentUserService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
