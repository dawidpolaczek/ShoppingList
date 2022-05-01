using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

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
            return View(_basketService.GetMany(b => b.UserId == _currentUser.GetId()));
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
                await _basketService.Add(basket);
                return RedirectToAction(nameof(Index));
            }

            return View(basket);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _basketService.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,
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
                    await _basketService.Update(basket);
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
            var basket = await _basketService.FindAsync(id);

            if (basket == null)
                return NotFound();

            await _basketService.Remove(basket);

            return RedirectToAction(nameof(Index));
        }
    }
}
