using dafstore.Domain.Contexts.OrderContext.Enums;
using dafstore.Domain.Contexts.OrderContext.ValueObjects;
using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.OrderContext.Entities;

public class Order : Entity
{
    private readonly List<OrderItem> _orderItems = new();
    
    private Order () { }
    
    public Order(Guid userId, DateTimeOffset orderDate, EOrderStatus orderStatus,
        IReadOnlyCollection<OrderItem> orderItems, EPaymentMethod paymentMethod,
        DeliveryAddress deliveryAddress)
    {
        UserId = userId;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
        _orderItems = orderItems?.ToList() ?? new List<OrderItem>();
        PaymentMethod = paymentMethod;
        DeliveryAddress = deliveryAddress;

        CalculeTotal();
    }

    public Guid UserId { get; }
    public DateTimeOffset OrderDate { get; }
    public EOrderStatus OrderStatus { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public EPaymentMethod PaymentMethod { get; }
    public decimal Total { get; private set; }
    public DeliveryAddress DeliveryAddress { get; }

    public void UpdateStatus(EOrderStatus status)
    {
        if (OrderStatus > status || OrderStatus == EOrderStatus.Canceled)
            return;

        OrderStatus = status;
    }
    
    private void CalculeTotal()
    {
        Total = _orderItems.Sum(i => i.Price * i.Quantity);
    }
}

public record OrderItem(Guid ProductId, int Quantity, decimal Price)
{
    public Guid Id { get; init; } = Guid.NewGuid();
}