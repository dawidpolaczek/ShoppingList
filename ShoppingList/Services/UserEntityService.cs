﻿using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public class UserEntityService<TEntity> : BaseDataService<TEntity>, IDataService<TEntity> where TEntity : EntityBase
    {
        private readonly IQueryable<TEntity> _userEntityService;

        public UserEntityService(ShoppingListDbContext dbContext, ICurrentUserService _currentUser)
            : base(dbContext)
        {
            var userId = _currentUser.GetId();
            _userEntityService = dbContext.Set<TEntity>().Where(e => e.UserId == userId);
        }

        public virtual async Task Add(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public virtual async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _userEntityService.FirstOrDefaultAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? filterExpression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var queryable = _userEntityService;

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
            return await _userEntityService.FirstOrDefaultAsync(b => b.Id == id) != null;
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
