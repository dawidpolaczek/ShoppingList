using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class DataService<TEntity> : BaseDataService<TEntity>, IDataService<TEntity> where TEntity : EntityBase
    {
        public DataService(ShoppingListDbContext dbContext)
            : base(dbContext) { }

        public virtual async Task AddOrUpdate(TEntity entity)
        {
            if (entity.Id > 0)
            {
                _dbContext.Update(entity);
            }
            else
            {
                _dbContext.Add(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _dbSet.FirstOrDefaultAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var queryable = _dbSet.AsNoTracking();

            if (filterExpression != null)
                queryable = queryable.Where(filterExpression);
            if (orderBy != null)
                queryable = orderBy(queryable);

            return await queryable.ToListAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public virtual void RemoveMany(ICollection<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public virtual async Task<bool> Exists(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        public virtual async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
