using System.Reflection;
using dafstore.Application.Shared.Abstractions;
using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Domain.Contexts.UserContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace dafstore.Infrastructure.Persistence.Shared;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Shirt> Shirts { get; set; }
    public DbSet<Pants> Pants { get; set; }
    public DbSet<Shorts> Shorts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCart { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}