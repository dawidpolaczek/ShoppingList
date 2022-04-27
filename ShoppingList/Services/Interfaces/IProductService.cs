using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingList.Models;
using System.Linq.Expressions;

namespace ShoppingList.Services.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Product? GetProduct(Expression<Func<Product, bool>> filterExpression);
        ICollection<Product> GetProducts(Expression<Func<Product, bool>>? filterExpression = null);
        Task RemoveProduct(Product product);
        Task RemoveProducts(ICollection<Product> products);
    }
}
