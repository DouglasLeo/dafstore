using AutoMapper;
using dafstore.Application.Orders.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Orders.Queries.GetOrders;

public record GetOrderByUserIdQuery(Guid UserId) : IRequest<OrderDTO>;

public class GetOrdersByUserQueryHandler : IRequestHandler<GetOrderByUserIdQuery, OrderDTO>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrdersByUserQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDTO> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<OrderDTO>(await _repository.GetOrderByUserIdAsync(request.UserId));
}