using dafstore.Application.Orders.Abstractions.Repositories;
using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Domain.Contexts.OrderContext.Enums;
using dafstore.Domain.Contexts.OrderContext.ValueObjects;
using MediatR;

namespace dafstore.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid  UserId,
    List<OrderItem> Items,
    EPaymentMethod PaymentMethod,
    DeliveryAddress DeliveryAddress)
    : IRequest<Guid>;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;

    public CreateOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request,CancellationToken cancellationToken = default)
    {
        var order = new Order(request.UserId, DateTimeOffset.Now, EOrderStatus.Pending, request.Items, 
            request.PaymentMethod, request.Items.Sum(i => i.Price * i.Quantity),request.DeliveryAddress);

        await _repository.AddAsync(order);
        await _repository.SaveChangesAsync();

        return order.Id;
    }
}