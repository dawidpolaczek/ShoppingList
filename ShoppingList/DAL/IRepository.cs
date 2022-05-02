using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.DAL
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task Save(TEntity entity);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterExpression);
        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null);
        Task Remove(TEntity entity);
        Task RemoveMany(ICollection<TEntity> entites);
        Task<bool> Exists(int id);
    }
}
