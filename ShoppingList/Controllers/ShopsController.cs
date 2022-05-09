using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using ShoppingList.ViewModels.Shop;

namespace ShoppingList.Controllers
{
    public class ShopsController : Controller
    {
        private readonly IDataService<Shop> _userShopsService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ShopsController(IDataService<Shop> userShopsService,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _userShopsService = userShopsService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index(string? searchString)
        {
            IEnumerable<Shop> shops = await _userShopsService.GetMany();

            if (!string.IsNullOrEmpty(searchString))
                shops = shops.Where(p => p.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var shopIndexViewModel = new ShopIndexViewModel
            {
                Shops = shops.Select(b => _mapper.Map<ShopTableViewModel>(b)).OrderBy(p => p.Name).ToList(),
            };

            return View(shopIndexViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var shopVm = new ShopCreateViewModel();
            return View(shopVm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShopCreateViewModel shopVm)
        {
            if (ModelState.IsValid)
            {
                var shop = new Shop()
                {
                    Name = shopVm.Name!,
                    Address = shopVm.Address,
                    UserId = _currentUserService.GetId()
                };
                await _userShopsService.Add(shop);
                await _userShopsService.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(shopVm);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _userShopsService.Get(p => p.Id == id);

            if (shop == null)
            {
                return NotFound();
            }

            var shopVm = _mapper.Map<Shop, ShopEditViewModel>(shop);

            return View(shopVm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int shopId, ShopEditViewModel shopVm)
        {
            if (shopId != shopVm.ShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var shop = await _userShopsService.Get(b => b.Id == shopVm.ShopId);
                    if (shop == null)
                        return NotFound();

                    shop.Name = shopVm.Name!;
                    shop.Address = shopVm.Address;
                    await _userShopsService.Update(shop);
                    await _userShopsService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _userShopsService.Exists(shopId))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(shopVm);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var Shop = await _userShopsService.Get(e => e.Id == id);

            if (Shop == null)
                return NotFound();

            var shopVm = _mapper.Map<Shop, ShopDetailsViewModel>(Shop);

            return View(shopVm);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var shop = await _userShopsService.Get(p => p.Id == id);

            if (shop == null)
                return NotFound();

            var shopVm = _mapper.Map<Shop, ShopDetailsViewModel>(shop);

            return View(shopVm);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shop = await _userShopsService.Get(p => p.Id == id);

            if (shop == null)
                return NotFound();

            shop.Baskets?.Clear();
            _userShopsService.Remove(shop);
            await _userShopsService.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
