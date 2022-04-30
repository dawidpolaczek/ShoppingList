using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using ShoppingList.Services;
using ShoppingList.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingList.Controllers
{
    public class BasketController : Controller
    {

        private readonly IDataService<Basket> _basketService;
        private readonly ICurrentUserService _currentUser;

        public BasketController(IDataService<Basket> basketService, ICurrentUserService currentUserService)
        {
            _basketService = basketService;
            _currentUser = currentUserService;
        }

        public string Index()
        {
            return _currentUser.GetId() ?? "null";
        }
    }
}
