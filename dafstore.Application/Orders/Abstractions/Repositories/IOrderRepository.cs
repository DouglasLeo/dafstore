using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Domain.Contexts.OrderContext.Entities;

namespace dafstore.Application.Orders.Abstractions.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
}