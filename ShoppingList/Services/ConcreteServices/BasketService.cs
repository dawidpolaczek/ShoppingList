using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class BasketService : BaseService, IBasketService
    {
        public BasketService(ShoppingListContext dbContext) : base(dbContext) { }

        public async Task<int> AddOrUpdateBasket(Basket basket)
        {
            _dbContext.Update(basket);
            return await _dbContext.SaveChangesAsync();
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

        public async Task<int> RemoveBasket(Basket basket)
        {
            _dbContext.Remove(basket);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveBaskets(ICollection<Basket> baskets)
        {
            _dbContext.RemoveRange(baskets);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
