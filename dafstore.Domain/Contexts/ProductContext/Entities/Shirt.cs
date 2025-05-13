using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Shirt : Product
{
    private Shirt() : base() { }

    public Shirt(Guid stockId, string name, string description, decimal price, ESize size, string[] colors,
        IEnumerable<Category> categories,
        EShirtCategory shirtCategory, IEnumerable<string> imagesKeys, bool inStock = false) :
        base(stockId, name, description, price, size, colors, categories, imagesKeys, inStock)
    {
        ShirtCategory = shirtCategory;
    }

    public EShirtCategory ShirtCategory { get; private set; }

    public void UpdateCategory(EShirtCategory category) => ShirtCategory = category;
}