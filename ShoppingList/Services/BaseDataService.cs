using ShoppingList.DAL;

namespace ShoppingList.Services
{
    public abstract class BaseDataService
    {
        protected readonly ShoppingListDbContext _dbContext;

        protected BaseDataService(ShoppingListDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
