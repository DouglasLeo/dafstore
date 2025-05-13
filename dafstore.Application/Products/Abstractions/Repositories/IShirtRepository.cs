using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Abstractions.Repositories;

public interface IShirtRepository : IRepository<Shirt>
{
    Task<IEnumerable<Shirt>> GetAllByShirtsCategoryAsync(EShirtCategory shirtCategory);
}