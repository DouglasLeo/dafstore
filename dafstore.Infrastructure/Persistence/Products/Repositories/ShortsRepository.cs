using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;

namespace dafstore.Infrastructure.Persistence.Products.Repositories;

public class ShortsRepository : Repository<Shorts>, IShortsRepository
{
    public ShortsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Shorts>> GetAllByShortsTissueTypeAsync(EShortsTissueType shortsTissueType) =>
        await SearchAsync(s => s.ShortsTissueType == shortsTissueType);
}