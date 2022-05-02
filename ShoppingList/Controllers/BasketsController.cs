using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class BasketsController : Controller
    {

        private readonly IRepository<Basket> _basketService;
        private readonly ICurrentUserService _currentUser;

        public BasketsController(IRepository<Basket> basketService, ICurrentUserService currentUserService)
        {
            _basketService = basketService;
            _currentUser = currentUserService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string? shopName, string? searchString)
        {
            var baskets = await _basketService.GetMany(b => b.UserId == _currentUser.GetId());
            IEnumerable<string>? userShops = (from b in baskets select b.Shops)
                .SelectMany(shops => from s in shops select s.Name);

            if (!string.IsNullOrEmpty(searchString))
                baskets = baskets.Where(b => b.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(shopName))
                baskets = baskets.Where(b => b.Shops!.Any(
                    s => s.Name.Equals(shopName, StringComparison.OrdinalIgnoreCase)));

            var basketShopViewModel = new BasketShopViewModel
            {
                Baskets = baskets.ToList(),
                Shops = new SelectList(userShops?.Distinct().ToList())
            };

            return View(basketShopViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DayOfWeek")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                await _basketService.Save(basket);
                return RedirectToAction(nameof(Index));
            }

            return View(basket);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _basketService.Get(b => b.Id == id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,DayOfWeek")] Basket basket)
        {
            if (id != basket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _basketService.Save(basket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _basketService.Exists(id))
                        return NotFound();
                    
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(basket);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var basket = await _basketService.Get(b => b.Id == id);

            if (basket == null)
                return NotFound();

            return View(basket);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var basket = await _basketService.Get(b => b.Id == id);

            if (basket == null)
                return NotFound();

            return View(basket);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basket = await _basketService.Get(b => b.Id == id);

            if (basket == null)
                return NotFound();

            await _basketService.Remove(basket);

            return RedirectToAction(nameof(Index));
        }
    }
}
