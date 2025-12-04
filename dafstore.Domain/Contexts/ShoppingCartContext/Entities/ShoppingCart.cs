using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.ShoppingCartContext;

public class ShoppingCart : Entity
{
    private readonly List<ShoppingCartItem> _shoppingCartItems = [];

    private ShoppingCart() { }

    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }
    public IReadOnlyCollection<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;

    public void AddShoppingCartItem(ShoppingCartItem item)
    {
        _shoppingCartItems.Clear();
        _shoppingCartItems.Add(item);
    }

    public void UpdateShoppingCartItems(IEnumerable<ShoppingCartItem> items)
    {
        var itemsById = items.ToDictionary(i => i.ProductId);

        foreach (var shoppingCartItem in _shoppingCartItems)
        {
            if (!itemsById.TryGetValue(shoppingCartItem.ProductId, out var item)) 
                continue;

            shoppingCartItem.UpdatePrice(item.Price);
            shoppingCartItem.UpdateQuantity(item.Quantity);
        }
    }
}

public class ShoppingCartItem
{
    public ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity, decimal price)
    {
        Id = Guid.CreateVersion7();
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid Id { get; }
    public Guid ShoppingCartId { get; }
    public ShoppingCart ShoppingCart { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public void UpdatePrice(decimal price) => Price = price;

    public void UpdateQuantity(int quantity) => Quantity = quantity;
}