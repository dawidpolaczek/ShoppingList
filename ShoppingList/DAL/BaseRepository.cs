using ShoppingList.DAL;

namespace ShoppingList.DAL
{
    public abstract class BaseRepository
    {
        protected readonly ShoppingListDbContext _dbContext;

        protected BaseRepository(ShoppingListDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
