using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.ShoppingCartContext;

public class ShoppingCart : Entity
{
    private List<ShoppingCartItem> _shoppingCartItem = [];

    private ShoppingCart() { }
    
    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }
    public IReadOnlyCollection<ShoppingCartItem> ShoppingCartItems => _shoppingCartItem.ToArray();

    public void AddShoppingCartItem(IEnumerable<ShoppingCartItem> items) => _shoppingCartItem.AddRange(items);
}

public class ShoppingCartItem
{
    public ShoppingCartItem(Guid shoppingCartId,Guid productId, int quantity, decimal price)
    {
        Id = Guid.CreateVersion7();
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid Id { get; }
    public Guid ShoppingCartId { get; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public void UpdatePrice(decimal price) => Price = price;

    public void UpdateQuantity(int quantity) => Quantity = quantity;
}