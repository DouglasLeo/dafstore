using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Domain.Contexts.UserContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace dafstore.Application.Shared.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Shirt> Shirts { get; set; }
    DbSet<Pants> Pants { get; set; }
    DbSet<Shorts> Shorts { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<ShoppingCart> ShoppingCart { get; set; }
    DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    DbSet<User> Users { get; set; }
}