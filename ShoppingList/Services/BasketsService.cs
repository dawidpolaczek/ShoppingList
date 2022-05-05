using Microsoft.EntityFrameworkCore;
using ShoppingList.Controllers;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public class BasketsService : BaseDataService<Basket>, IDataService<Basket>
    {
        public BasketsService(ShoppingListDbContext dbContext)
            : base(dbContext) { }

        public virtual async Task Add(Basket basket)
        {
            await _dbContext.AddAsync(basket);
        }

        public virtual void Update(Basket basket)
        {
            _dbContext.Update(basket);
        }

        public virtual async Task<Basket?> Get(Expression<Func<Basket, bool>> filterExpression)
        {
            return await _dbSet.FirstOrDefaultAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<Basket>> GetMany(Expression<Func<Basket, bool>>? filterExpression = null,
            Func<IQueryable<Basket>, IOrderedQueryable<Basket>>? orderBy = null)
        {
            var queryable = _dbSet.AsNoTracking();

            if (filterExpression != null)
                queryable = queryable.Where(filterExpression);
            if (orderBy != null)
                queryable = orderBy(queryable);

            return await queryable.ToListAsync();
        }

        public virtual void Remove(Basket basket)
        {
            _dbContext.Remove(basket);
        }

        public virtual void RemoveMany(ICollection<Basket> baskets)
        {
            _dbContext.RemoveRange(baskets);
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
