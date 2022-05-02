using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class DataService<TEntity> : BaseDataService, IDataService<TEntity> where TEntity : class
    {
        public DataService(ShoppingListDbContext dbContext)
            : base(dbContext) { }

        public async Task Add(TEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filterExpression);
        }

        public async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null)
        {
            var queryable = _dbContext.Set<TEntity>().AsNoTracking();
            if (filterExpression != null)
                return await queryable.Where(filterExpression).ToListAsync();
            return await queryable.ToListAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveMany(ICollection<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> FindAsync(params object?[]? keyValues)
        {
            return await _dbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public async Task<bool> Exists(params object?[]? keyValues)
        {
            return await FindAsync(keyValues) != null;
        }
    }
}
