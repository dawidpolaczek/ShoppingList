using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingList.DAL;
using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public class BasketService : BaseService, IBasketService
    {
        public BasketService(ShoppingListContext dbContext) : base(dbContext) { }

        public async Task<bool> AddOrUpdateBasket(Basket basket)
        {
            _dbContext.Update(basket);
            await _dbContext.SaveChangesAsync();

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
    }
}
