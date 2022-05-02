using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class GenericDataService<TEntity> : BaseDataService, IDataService<TEntity> where TEntity : EntityBase
    {
        public GenericDataService(ShoppingListDbContext dbContext)
            : base(dbContext) { }

        public virtual async Task Save(TEntity entity)
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
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var queryable = _dbContext.Set<TEntity>().AsNoTracking();

            if (filterExpression != null)
                queryable = queryable.Where(filterExpression);
            if (orderBy != null)
                queryable = orderBy(queryable);

            return await queryable.ToListAsync();
        }

        public virtual async Task Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveMany(ICollection<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<bool> Exists(int id)
        {
            return await Get(e => e.Id == id) != null;
        }
    }
}
