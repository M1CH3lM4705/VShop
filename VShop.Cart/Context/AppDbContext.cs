using Microsoft.EntityFrameworkCore;
using VShop.Cart.Models;

namespace VShop.Cart.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Product>? Products {get; set;}

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CartHeader> CartHeaders {get; set;}

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
