using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Shorts : Product
{
    private Shorts () : base() { }
    
    public Shorts(Guid stockId, string name, string description, decimal price, ESize size,EShortsTissueType shortsTissueType, string[] colors, IEnumerable<Category> categories, IEnumerable<string> imagesKeys, bool inStock = false) 
        : base(stockId, name, description, price, size, colors, categories,imagesKeys, inStock)
    {
        ShortsTissueType = shortsTissueType;
    }

    public EShortsTissueType ShortsTissueType { get; }
}