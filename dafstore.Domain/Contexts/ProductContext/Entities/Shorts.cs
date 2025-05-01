using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Shorts : Product
{
    public Shorts(long stockId, string name, string description, decimal price, ESize size,EShortsTissueType shortsTissueType, string color, IEnumerable<string> imagesKeys, bool inStock = false) 
        : base(stockId, name, description, price, size, color, imagesKeys, inStock)
    {
        ShortsTissueType = shortsTissueType;
    }

    public EShortsTissueType ShortsTissueType { get; }
}