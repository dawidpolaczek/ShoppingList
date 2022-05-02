using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class ShopsController : Controller
    {
        private readonly IRepository<Shop> _shopService;
        private readonly ICurrentUserService _currentUser;

        public ShopsController(IRepository<Shop> shopService, ICurrentUserService currentUserService)
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
