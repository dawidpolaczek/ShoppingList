using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services.Interfaces
{
    public interface IDataService<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        TEntity? Get(Expression<Func<TEntity, bool>> filterExpression);
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>>? filterExpression = null);
        Task Remove(TEntity entity);
        Task RemoveRange(ICollection<TEntity> entites);
    }
}
