using ShoppingList.Data;
using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public class BasketService : BaseService, IBasketService
    {
        public BasketService(ShoppingListContext dbContext) : base(dbContext) { }

        public Basket AddBasket(Basket basket)
        {
            throw new NotImplementedException();
        }

        public Basket UpdateBasket(Basket basket)
        {
            throw new NotImplementedException();
        }

        public Basket GetBasket(Expression<Func<Basket, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public ICollection<Basket> GetBaskets(Expression<Func<Basket, bool>>? filterExpression = null)
        {
            throw new NotImplementedException();
        }
    }
}
