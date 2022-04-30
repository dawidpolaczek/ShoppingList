using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDataService<Product> _productService;
        private readonly ICurrentUserService _currentUser;

        public ProductsController(IDataService<Product> productService, ICurrentUserService currentUserService)
        {
            _productService = productService;
            _currentUser = currentUserService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
