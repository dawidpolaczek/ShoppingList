using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class BasketsController : BaseCrudController<Basket>
    {
        public BasketsController(IDataService<Basket> basketService, ICurrentUserService currentUserService)
            : base(basketService, currentUserService) { }

        [Authorize]
        public async Task<IActionResult> Index(string? shopName, string? searchString)
        {
            var baskets = await _dataService.GetMany(b => b.UserId == _currentUser.GetId(),
                bs => bs.OrderBy(b => b.DayOfWeek));

            var userShops = (from b in baskets where b.Shops != null select b.Shops)
                .SelectMany(shops => from s in shops select s.Name);

            if (!string.IsNullOrEmpty(searchString))
                baskets = baskets.Where(b => b.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(shopName))
                baskets = baskets.Where(b => b.Shops?.Any(
                    s => s.Name.Equals(shopName, StringComparison.OrdinalIgnoreCase)) ?? false);

            var basketShopViewModel = new BasketShopViewModel
            {
                Baskets = baskets.ToList(),
                Shops = new SelectList(userShops?.Distinct().ToList())
            };

            return View(basketShopViewModel);
        }
    }
}
