using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;

namespace dafstore.Infrastructure.Persistence.Products.Repositories;

public class PantsRepository : Repository<Pants>, IPantsRepository
{
    public PantsRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Pants>> GetAllByPantsTissueTypeAsync(EPantsTissueType pantsTissueType) =>
        await SearchAsync(s => s.TissueType == pantsTissueType);
}