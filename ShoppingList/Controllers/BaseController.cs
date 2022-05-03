using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Controllers
{
    public abstract class BaseController<TEntity> : Controller where TEntity : EntityBase
    {
        protected readonly IDataService<TEntity> _dataService;
        protected readonly ICurrentUserService _currentUser;

        public BaseController(IDataService<TEntity> dataService, ICurrentUserService currentUser)
        {
            _dataService = dataService;
            _currentUser = currentUser;
        }

        [Authorize]
        public virtual IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _dataService.AddOrUpdate(entity);
                return RedirectToAction(nameof(Index));
            }

            return View(entity);
        }

        public virtual async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _dataService.Get(e => e.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.AddOrUpdate(entity);
                    await _dataService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _dataService.Exists(id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(entity);
        }

        [Authorize]
        public virtual async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = await _dataService.Get(e => e.Id == id);

            if (entity == null)
                return NotFound();

            return View(entity);
        }

        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = await _dataService.Get(e => e.Id == id);

            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _dataService.Get(e => e.Id == id);

            if (entity == null)
                return NotFound();

            _dataService.Remove(entity);
            await _dataService.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
