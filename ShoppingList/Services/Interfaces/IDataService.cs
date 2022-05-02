using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services.Interfaces
{
    public interface IDataService<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterExpression);
        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null);
        Task Remove(TEntity entity);
        Task RemoveMany(ICollection<TEntity> entites);
        Task<TEntity?> FindAsync(params object?[]? keyValues);
        Task<bool> Exists(params object?[]? keyValues);
    }
}
