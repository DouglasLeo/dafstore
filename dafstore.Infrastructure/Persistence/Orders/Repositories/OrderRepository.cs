using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;

namespace dafstore.Infrastructure.Persistence.Orders.Repositories;

public class OrderRepository : Repository<Order>
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId) =>
        await SearchAsync(o => o.UserId == userId);
}