using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Shirt : Product
{
    public Shirt(long stockId, string name, string description, decimal price, ESize size, string color,
        EShirtCategory category, IEnumerable<string> imagesKeys, bool inStock = false) :
        base(stockId, name, description, price, size, color, imagesKeys, inStock)
    {
        Category = category;
    }

    public EShirtCategory Category { get; private set; }

    public void UpdateCategory(EShirtCategory category) => Category = category;
}