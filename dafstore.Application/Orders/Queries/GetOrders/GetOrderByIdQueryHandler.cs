using AutoMapper;
using dafstore.Application.Orders.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Orders.Queries.GetOrders;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDTO>;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;


    public GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<OrderDTO>(await _repository.GetOrderByIdAsync(request.Id));
}