using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class BasketService : BaseService, IBasketService
    {
        public BasketService(ShoppingListContext dbContext) : base(dbContext) { }

        public async Task AddBasket(Basket basket)
        {
            _dbContext.Add(basket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateBasket(Basket basket)
        {
            try
            {
                _dbContext.Update(basket);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public Basket? GetBasket(Expression<Func<Basket, bool>> filterExpression)
        {
            return _dbContext.Baskets.FirstOrDefault(filterExpression);
        }

        public ICollection<Basket> GetBaskets(Expression<Func<Basket, bool>>? filterExpression = null)
        {
            if (filterExpression != null)
                return _dbContext.Baskets.Where(filterExpression).ToList();
            return _dbContext.Baskets.ToList();
        }

        public async Task RemoveBasket(Basket basket)
        {
            _dbContext.Remove(basket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveBaskets(ICollection<Basket> baskets)
        {
            _dbContext.RemoveRange(baskets);
            await _dbContext.SaveChangesAsync();
        }
    }
}
