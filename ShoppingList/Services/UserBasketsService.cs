using Microsoft.EntityFrameworkCore;
using ShoppingList.Controllers;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public class UserBasketsService : BaseDataService<Basket>, IDataService<Basket>
    {
        private readonly IQueryable<Basket> _userBasketsQuery;

        public UserBasketsService(ShoppingListDbContext dbContext, ICurrentUserService _currentUser)
            : base(dbContext)
        {
            var userId = _currentUser.GetId();
            _userBasketsQuery = dbContext.Set<Basket>().Where(b => b.UserId == userId);
        }

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
            return await _userBasketsQuery.FirstOrDefaultAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<Basket>> GetMany(Expression<Func<Basket, bool>>? filterExpression = null,
            Func<IQueryable<Basket>, IOrderedQueryable<Basket>>? orderBy = null)
        {
            var queryable = _userBasketsQuery;

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
            return await _userBasketsQuery.FirstOrDefaultAsync(b => b.Id == id) != null;
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
