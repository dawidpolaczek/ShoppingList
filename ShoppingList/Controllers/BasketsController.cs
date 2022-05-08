using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingList.ViewModels.Basket;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using ShoppingList.Helpers;

namespace ShoppingList.Controllers
{
    public class BasketsController : Controller
    {
        private readonly IDataService<Basket> _userBasketsService;
        private readonly IDataService<Shop> _userShopsService;
        private readonly IDataService<Product> _userProductsService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public BasketsController(IDataService<Basket> userBasketsService,
            IDataService<Shop> userShopsService,
            IDataService<Product> userProductsService,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _userBasketsService = userBasketsService;
            _userShopsService = userShopsService;
            _userProductsService = userProductsService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index(string? shopName, string? searchString)
        {
            IEnumerable<Basket> baskets = await _userBasketsService.GetMany();

            var userShops = baskets.Where(b => b.Shop != null).Select(b => b.Shop);

            if (!string.IsNullOrEmpty(searchString))
                baskets = baskets.Where(b => b.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(shopName))
                baskets = baskets.Where(b => b.Shop != null && b.Shop.Name.Equals(shopName, StringComparison.OrdinalIgnoreCase));

            var basketIndexViewModel = new BasketIndexViewModel
            {
                Baskets = baskets.Select(b => _mapper.Map<BasketTableViewModel>(b)).OrderBy(b => b.NextShoppingDate).ToList(),
                Shops = new SelectList(userShops?.Distinct().ToList())
            };

            return View(basketIndexViewModel);
        }

        [Authorize]
        public virtual async Task<IActionResult> Create()
        {
            var userShops = await _userShopsService.GetMany(orderBy: ss => ss.OrderBy(s => s.Name));
            var userProducts = await _userProductsService.GetMany(orderBy: ps => ps.OrderBy(s => s.Name));

            var basket = new BasketCreateViewModel()
            {
                ShopsList = userShops.ToSelectList(additionaInfo: s => s.Address != null ? $", {s.Address}" : ""),
                ProductsList = userProducts.ToMultiSelectList(),
            };

            return View(basket);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(BasketCreateViewModel basketVm)
        {
            if (ModelState.IsValid)
            {
                var basket = new Basket()
                {
                    Name = basketVm.Name!,
                    DayEveryWeek = basketVm.DayEveryWeek,
                    SpecificDate = basketVm.SpecificDate,
                    Products = await _userProductsService.GetManyById(basketVm.SelectedProductsIds),
                    Shop = await _userShopsService.Get(s => s.Id == basketVm.SelectedShopId),
                    UserId = _currentUserService.GetId(),
                };
                await _userBasketsService.Add(basket);
                await _userBasketsService.Save();
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

            var basket = await _userBasketsService.Get(b => b.Id == id);

            if (basket == null)
            {
                return NotFound();
            }

            var userShops = await _userShopsService.GetMany(orderBy: ss => ss.OrderBy(s => s.Name));
            var userProducts = await _userProductsService.GetMany(orderBy: ps => ps.OrderBy(s => s.Name));

            var basketVm = _mapper.Map<Basket, BasketEditViewModel>(basket);
            basketVm.ShopsList = userShops.ToSelectList(basket.Shop, s => s.Address != null ? $", {s.Address}" : "");

            basketVm.ProductsList = userProducts.ToMultiSelectList(basket.Products);

            return View(basketVm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int basketId, BasketEditViewModel basketVm)
        {
            if (basketId != basketVm.BasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var basket = await _userBasketsService.Get(b => b.Id == basketVm.BasketId);
                    if (basket == null)
                        return NotFound();
                    
                    basket.Products?.Clear();

                    basket.Name = basketVm.Name!;
                    basket.DayEveryWeek = basketVm.DayEveryWeek;
                    basket.SpecificDate = basketVm.SpecificDate;
                    basket.Products = await _userProductsService.GetManyById(basketVm.SelectedProductsIds);
                    basket.ShopId = basketVm.SelectedShopId;
                    await _userBasketsService.Update(basket);
                    await _userBasketsService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _userBasketsService.Exists(basketId))
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

            var basket = await _userBasketsService.Get(e => e.Id == id);

            if (basket == null)
                return NotFound();

            var basketVm = _mapper.Map<Basket, BasketDetailsViewModel>(basket);

            return View(basketVm);
        }

        [Authorize]
        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var basket = await _userBasketsService.Get(e => e.Id == id);

            if (basket == null)
                return NotFound();

            var basketVm = _mapper.Map<Basket, BasketDetailsViewModel>(basket);

            return View(basketVm);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basket = await _userBasketsService.Get(e => e.Id == id);

            if (basket == null)
                return NotFound();

            _userBasketsService.Remove(basket);
            await _userBasketsService.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
