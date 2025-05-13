using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;

namespace dafstore.Infrastructure.Persistence.Products.Repositories;

public class ShirtRepository :  Repository<Shirt>, IShirtRepository
{
    public ShirtRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Shirt>> GetAllByShirtsCategoryAsync(EShirtCategory shirtCategory) =>
        await SearchAsync(s => s.ShirtCategory == shirtCategory);
}