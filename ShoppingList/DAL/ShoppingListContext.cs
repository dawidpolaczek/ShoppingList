#nullable disable
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.DAL;

public class ShoppingListContext : IdentityDbContext<User>
{
    public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Product> Products { get; set; }
}
