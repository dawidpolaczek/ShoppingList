using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public interface IBasketService
    {
        public Basket AddBasket(Basket basket);
        public Basket UpdateBasket(Basket basket);
        public Basket GetBasket(Expression<Func<Basket, bool>> filterExpression);
        public ICollection<Basket> GetBaskets(Expression<Func<Basket, bool>>? filterExpression = null);
    }
}
