#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.Data;

public class ShoppingListContext : IdentityDbContext<User>
{
    public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Product> Products { get; set; }
}
