using dafstore.Application.Orders.Abstractions.Repositories;
using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dafstore.Infrastructure.Persistence.Orders.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id) =>
        await DbSet.AsNoTracking().SingleOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetOrderByUserIdAsync(Guid userId) =>
        await SearchAsync(o => o.UserId == userId);
}