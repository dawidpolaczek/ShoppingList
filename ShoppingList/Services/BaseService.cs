using ShoppingList.Data;

namespace ShoppingList.Services
{
    public abstract class BaseService
    {
        protected ShoppingListContext DbContext { get; }

        public BaseService(ShoppingListContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
