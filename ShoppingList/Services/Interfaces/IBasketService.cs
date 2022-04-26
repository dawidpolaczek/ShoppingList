using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services.Interfaces
{
    public interface IBasketService
    {
        Task<int> AddOrUpdateBasket(Basket basket);
        Basket? GetBasket(Expression<Func<Basket, bool>> filterExpression);
        ICollection<Basket> GetBaskets(Expression<Func<Basket, bool>>? filterExpression = null);
        Task<int> RemoveBasket(Basket basket);
        Task<int> RemoveBaskets(ICollection<Basket> baskets);
    }
}
