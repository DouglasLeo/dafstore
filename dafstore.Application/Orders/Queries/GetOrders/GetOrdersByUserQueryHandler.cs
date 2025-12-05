using AutoMapper;
using dafstore.Application.Orders.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Orders.Queries.GetOrders;

public record GetOrderByUserIdQuery(Guid UserId) : IRequest<IEnumerable<OrderDTO>>;

public class GetOrdersByUserQueryHandler : IRequestHandler<GetOrderByUserIdQuery, IEnumerable<OrderDTO>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrdersByUserQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<OrderDTO>>(await _repository.GetOrderByUserIdAsync(request.UserId));
}