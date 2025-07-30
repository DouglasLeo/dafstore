using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Queries.GetShirt;

public class ShirtDTO : ProductDTO
{
    public EShirtCategory ShirtCategory { get; init; }
}