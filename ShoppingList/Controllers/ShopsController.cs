using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class ShopsController : Controller
    {
        private readonly IDataService<Shop> _shopService;
        private readonly ICurrentUserService _currentUser;

        public ShopsController(IDataService<Shop> shopService, ICurrentUserService currentUserService)
        {
            _shopService = shopService;
            _currentUser = currentUserService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
