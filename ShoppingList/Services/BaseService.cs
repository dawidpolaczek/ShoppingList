using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingList.DAL;

namespace ShoppingList.Services
{
    public abstract class BaseService
    {
        protected readonly ShoppingListContext _dbContext;

        public BaseService(ShoppingListContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
