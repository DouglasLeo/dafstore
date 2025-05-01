using dafstore.Domain.Contexts.OrderContext.Enums;
using dafstore.Domain.Contexts.OrderContext.ValueObjects;
using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.OrderContext.Entities;

public class Order : Entity
{
    public Order(Guid userId, DateTimeOffset orderDate, EOrderStatus orderStatus,
        IReadOnlyCollection<OrderItem> orderItems, EPaymentMethod paymentMethod, float total,
        DeliveryAddress deliveryAddress)
    {
        UserId = userId;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
        OrderItems = orderItems;
        PaymentMethod = paymentMethod;
        Total = total;
        DeliveryAddress = deliveryAddress;
    }

    public Guid UserId { get; }
    public DateTimeOffset OrderDate { get; }
    public EOrderStatus OrderStatus { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems { get; }
    public EPaymentMethod PaymentMethod { get; }
    public float Total { get; }
    public DeliveryAddress DeliveryAddress { get; }

    public void UpdateStatus(EOrderStatus status)
    {
        if (OrderStatus > status || OrderStatus == EOrderStatus.Canceled)
            return;

        OrderStatus = status;
    }
}

public record OrderItem(Guid ProductId, int Quantity, decimal Price);