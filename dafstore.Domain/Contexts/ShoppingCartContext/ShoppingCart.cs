using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.ShoppingCartContext;

public class ShoppingCart : Entity
{
    private List<ShoppingCartItem> _shoppingCartItem = [];

    public ShoppingCart(Guid userId, IEnumerable<ShoppingCartItem> shoppingCartItems)
    {
        UserId = userId;
        AddShoppingCartItem(shoppingCartItems);
    }

    public Guid UserId { get; set; }
    public IReadOnlyCollection<ShoppingCartItem> ShoppingCartItems => _shoppingCartItem.ToArray();

    public void AddShoppingCartItem(IEnumerable<ShoppingCartItem> items) => _shoppingCartItem.AddRange(items);
}

public class ShoppingCartItem
{
    public ShoppingCartItem(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public void UpdatePrice(decimal price) => Price = price;

    public void UpdateQuantity(int quantity) => Quantity = quantity;
}