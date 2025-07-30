using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Queries.GetShorts;

public class ShortsDTO : ProductDTO
{
    public EShortsTissueType ShortsTissueType { get; init; }
}