using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddBasket(Basket basket);
        Task<bool> UpdateBasket(Basket basket);
        Basket? GetBasket(Expression<Func<Basket, bool>> filterExpression);
        ICollection<Basket> GetBaskets(Expression<Func<Basket, bool>>? filterExpression = null);
        Task RemoveBasket(Basket basket);
        Task RemoveBaskets(ICollection<Basket> baskets);
    }
}
