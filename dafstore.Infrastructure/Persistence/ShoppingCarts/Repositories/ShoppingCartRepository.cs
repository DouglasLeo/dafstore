using dafstore.Application.ShoppingCarts.Abstractions.Repository;
using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;

namespace dafstore.Infrastructure.Persistence.ShoppingCarts.Repositories;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ShoppingCart?> GetByUserIdAsync(Guid id) => await DbSet.FindAsync(id);
}