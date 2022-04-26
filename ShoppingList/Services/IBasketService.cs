using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public interface IBasketService
    {
        Task<bool> AddOrUpdateBasket(Basket basket);
        Basket? GetBasket(Expression<Func<Basket, bool>> filterExpression);
        ICollection<Basket> GetBaskets(Expression<Func<Basket, bool>>? filterExpression = null);
    }
}
