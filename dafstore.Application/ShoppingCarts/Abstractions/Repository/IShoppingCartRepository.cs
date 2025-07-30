using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Domain.Contexts.ShoppingCartContext;

namespace dafstore.Application.ShoppingCarts.Abstractions.Repository;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    Task<ShoppingCart?> GetByUserIdAsync(Guid id);
}