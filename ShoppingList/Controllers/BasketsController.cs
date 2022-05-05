using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingList.ViewModels.Basket;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public class BasketsController : Controller
    {
        private readonly IDataService<Basket> _basketService;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public BasketsController(IDataService<Basket> basketService,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _basketService = basketService;
            _currentUser = currentUserService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index(string? shopName, string? searchString)
        {
            var baskets = await _basketService.GetMany(b => b.UserId == _currentUser.GetId(),
                bs => bs.OrderBy(b => b.DayEveryWeek));

            var userShops = (from b in baskets where b.Shops != null select b.Shops)
                .SelectMany(shops => from s in shops select s.Name);

            if (!string.IsNullOrEmpty(searchString))
                baskets = baskets.Where(b => b.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(shopName))
                baskets = baskets.Where(b => b.Shops?.Any(
                    s => s.Name.Equals(shopName, StringComparison.OrdinalIgnoreCase)) ?? false);

            var basketShopViewModel = new BasketSearchViewModel
            {
                Baskets = baskets.Select(b => _mapper.Map<BasketTableViewModel>(b)).ToList(),
                Shops = new SelectList(userShops?.Distinct().ToList())
            };

            return View(basketShopViewModel);
        }

        [Authorize]
        public virtual IActionResult Create()
        {
            var basket = new BasketCreateViewModel() { UserId = _currentUser.GetId() };
            return View(basket);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(BasketCreateViewModel basketVm)
        {
            if (ModelState.IsValid)
            {
                var basket = _mapper.Map<Basket>(basketVm);
                await _basketService.Add(basket);
                await _basketService.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(basketVm);
        }

        [Authorize]
        public virtual async Task<IActionResult> Edit(int? id)
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

            var basketVm = _mapper.Map<Basket, BasketEditViewModel>(basket);

            return View(basketVm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int id, BasketEditViewModel basketVm)
        {
            if (id != basketVm.BasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var basket = _mapper.Map<BasketEditViewModel, Basket>(basketVm);
                    _basketService.Update(basket);
                    await _basketService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _basketService.Exists(id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(basketVm);
        }

        [Authorize]
        public virtual async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var basket = await _basketService.Get(e => e.Id == id);

            if (basket == null)
                return NotFound();

            return View(basket);
        }

        [Authorize]
        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var basket = await _basketService.Get(e => e.Id == id);

            if (basket == null)
                return NotFound();

            return View(basket);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basket = await _basketService.Get(e => e.Id == id);

            if (basket == null)
                return NotFound();

            _basketService.Remove(basket);
            await _basketService.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
