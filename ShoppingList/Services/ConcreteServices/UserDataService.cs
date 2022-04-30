using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class UserDataService<TEntity> : BaseDataService, IDataService<TEntity> where TEntity : class
    {
        public UserDataService(ShoppingListContext dbContext)
            : base(dbContext) { }

        public async Task Add(TEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(filterExpression);
        }

        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>>? filterExpression = null)
        {
            if (filterExpression != null)
                return _dbContext.Set<TEntity>().Where(filterExpression).AsNoTracking().ToList();
            return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public async Task Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRange(ICollection<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
