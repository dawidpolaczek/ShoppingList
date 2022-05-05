using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services.Interfaces
{
    public interface IDataService<TEntity>
        where TEntity : EntityBase
    {
        Task Add(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterExpression);
        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        void Remove(TEntity entity);
        void RemoveMany(ICollection<TEntity> entites);
        Task<bool> Exists(int id);
        Task Save();
    }
}
