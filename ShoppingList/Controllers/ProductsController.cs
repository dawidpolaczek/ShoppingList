using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using ShoppingList.ViewModels.Product;

namespace ShoppingList.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDataService<Product> _userProductsService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ProductsController(IDataService<Product> userProductsService,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _userProductsService = userProductsService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index(string? searchString)
        {
            IEnumerable<Product> products = await _userProductsService.GetMany();

            if (!string.IsNullOrEmpty(searchString))
                products = products.Where(p => p.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var productIndexViewModel = new ProductIndexViewModel
            {
                Products = products.Select(b => _mapper.Map<ProductTableViewModel>(b)).OrderBy(p => p.Name).ToList(),
            };

            return View(productIndexViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var productVm = new ProductCreateViewModel();
            return View(productVm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = productVm.Name!,
                    Description = productVm.Description,
                    UserId = _currentUserService.GetId()
                };
                await _userProductsService.Add(product);
                await _userProductsService.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(productVm);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _userProductsService.Get(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productVm = _mapper.Map<Product, ProductEditViewModel>(product);

            return View(productVm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int productId, ProductEditViewModel productVm)
        {
            if (productId != productVm.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _userProductsService.Get(b => b.Id == productVm.ProductId);
                    if (product == null)
                        return NotFound();

                    product.Name = productVm.Name!;
                    product.Description = productVm.Description;
                    await _userProductsService.Update(product);
                    await _userProductsService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _userProductsService.Exists(productId))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(productVm);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _userProductsService.Get(e => e.Id == id);

            if (product == null)
                return NotFound();

            var productVm = _mapper.Map<Product, ProductDetailsViewModel>(product);

            return View(productVm);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _userProductsService.Get(p => p.Id == id);

            if (product == null)
                return NotFound();

            var productVm = _mapper.Map<Product, ProductDetailsViewModel>(product);

            return View(productVm);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _userProductsService.Get(p => p.Id == id);

            if (product == null)
                return NotFound();

            _userProductsService.Remove(product);
            await _userProductsService.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
