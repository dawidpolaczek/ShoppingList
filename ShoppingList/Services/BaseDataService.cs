using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;

namespace ShoppingList.Services
{
    public abstract class BaseDataService<TEntity> where TEntity : EntityBase
    {
        protected readonly ShoppingListDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseDataService(ShoppingListDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
    }
}
