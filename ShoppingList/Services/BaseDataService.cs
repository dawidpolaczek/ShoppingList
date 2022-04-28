using ShoppingList.DAL;

namespace ShoppingList.Services
{
    public abstract class BaseDataService
    {
        protected readonly ShoppingListContext _dbContext;

        protected BaseDataService(ShoppingListContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
