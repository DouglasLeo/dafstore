using dafstore.Application.Orders.Abstractions.Repositories;
using dafstore.Domain.Contexts.OrderContext.Enums;
using MediatR;

namespace dafstore.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid Id, EOrderStatus Status) : IRequest<Guid>;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;

    public UpdateOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken = default)
    {
        var order = await _repository.FindByIdAsync(request.Id);
        if (order is null) return Guid.Empty;

        order.UpdateStatus(request.Status);
        await _repository.SaveChangesAsync();

        return order.Id;
    }
}