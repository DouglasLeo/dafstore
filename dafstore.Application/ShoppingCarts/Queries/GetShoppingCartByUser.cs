using AutoMapper;
using dafstore.Application.ShoppingCarts.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.ShoppingCarts.Queries;

public record GetShoppingCartByUserIdQuery(Guid UserId) : IRequest<ShoppingCartDTO>;

public class GetShoppingCartByUser : IRequestHandler<GetShoppingCartByUserIdQuery, ShoppingCartDTO>
{
    private readonly IShoppingCartRepository _repository;
    private readonly IMapper _mapper;

    public GetShoppingCartByUser(IShoppingCartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ShoppingCartDTO> Handle(GetShoppingCartByUserIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<ShoppingCartDTO>(await _repository.GetByUserIdAsync(request.UserId));
}