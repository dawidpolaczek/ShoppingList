using Microsoft.EntityFrameworkCore;
using ShoppingList.DAL;
using ShoppingList.Models;
using ShoppingList.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingList.Services.ConcreteServices
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(ShoppingListContext dbContext) : base(dbContext) { }

        public async Task AddProduct(Product product)
        {
            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _dbContext.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public Product? GetProduct(Expression<Func<Product, bool>> filterExpression)
        {
            return _dbContext.Products.FirstOrDefault(filterExpression);
        }

        public ICollection<Product> GetProducts(Expression<Func<Product, bool>>? filterExpression = null)
        {
            if (filterExpression != null)
                return _dbContext.Products.Where(filterExpression).ToList();
            return _dbContext.Products.ToList();
        }

        public async Task RemoveProduct(Product product)
        {
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProducts(ICollection<Product> products)
        {
            _dbContext.RemoveRange(products);
            await _dbContext.SaveChangesAsync();
        }
    }
}
