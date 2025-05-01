using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Pants : Product
{
    public Pants(long stockId, string name, string description, decimal price, ESize size, string color,
        EPantsTissueType tissueType, IEnumerable<string> imagesKeys, bool inStock = false)
        : base(stockId, name, description, price, size, color, imagesKeys, inStock)
    {
        TissueType = tissueType;
    }

    public EPantsTissueType TissueType { get; }
}