using AutoMapper;
using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Domain.Contexts.OrderContext.Enums;
using dafstore.Domain.Contexts.OrderContext.ValueObjects;

namespace dafstore.Application.Orders.Queries.GetOrders;

public class OrderDTO
{
    public DateTimeOffset OrderDate { get; init; }
    public EOrderStatus OrderStatus { get; init; }
    public IReadOnlyCollection<OrderItem> OrderItems { get; init; }
    public decimal Total { get; init; }
    public DeliveryAddress DeliveryAddress { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, OrderDTO>();
        }
    }
}