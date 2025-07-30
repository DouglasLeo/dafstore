using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Queries;

public abstract class ProductDTO
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public ESize Size { get; init; }
    public string[] Colors { get; init; }
}