using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Pants : Product
{
    private Pants() : base() { }

    public Pants(Guid stockId, string name, string description, decimal price, ESize size, string[] colors,
        IEnumerable<Category> categories,
        EPantsTissueType tissueType, IEnumerable<string> imagesKeys, bool inStock = false)
        : base(stockId, name, description, price, size, colors, categories, imagesKeys, inStock)
    {
        TissueType = tissueType;
    }

    public EPantsTissueType TissueType { get; }
}