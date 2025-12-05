using AutoMapper;
using dafstore.Application.Orders.Abstractions.Repositories;
using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Domain.Contexts.OrderContext.Enums;
using dafstore.Domain.Contexts.OrderContext.ValueObjects;
using MediatR;

namespace dafstore.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    List<OrderItem> Items,
    EPaymentMethod PaymentMethod,
    DeliveryAddressDTO DeliveryAddress)
    : IRequest<Guid>;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken = default)
    {
        var order = new Order(request.UserId, DateTimeOffset.UtcNow, EOrderStatus.Pending, request.Items,
            request.PaymentMethod,
            _mapper.Map<DeliveryAddress>(request.DeliveryAddress));

        await _repository.AddAsync(order);
        await _repository.SaveChangesAsync();

        return order.Id;
    }
}