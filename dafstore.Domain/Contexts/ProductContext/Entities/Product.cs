using dafstore.Domain.Contexts.ProductContext.Enums;
using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public abstract class Product : Entity
{
    private List<string> _imageKeys = [];

    protected Product(long stockId, string name, string description, decimal price, ESize size,
        string color,
        IEnumerable<string> imagesKeys, bool inStock)
    {
        StockId = stockId;
        InStock = inStock;
        Name = name;
        Description = description;
        Price = price;
        Size = size;
        Color = color;

        AddImages(imagesKeys);
    }

    public long StockId { get; }
    public bool InStock { get; private set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public decimal Price { get; protected set; }
    public ESize Size { get; protected set; }
    public string Color { get; protected set; }
    public IReadOnlyCollection<string> ImageKeys => _imageKeys.ToArray();

    public void AddImages(IEnumerable<string> imagesKeys) => _imageKeys.AddRange(imagesKeys);

    public void UpdateName(string name) => Name = name;
    public void UpdateDescription(string description) => Description = description;
    public void UpdatePrice(decimal price) => Price = price;
    public void UpdateSize(ESize size) => Size = size;
    public void UpdateColor(string color) => Color = color;

    public void UpdateInStock(bool inStock) => InStock = inStock;
}