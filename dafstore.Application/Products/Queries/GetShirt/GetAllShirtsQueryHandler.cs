using AutoMapper;
using dafstore.Application.Products.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Products.Queries.GetShirt;

public record GetAllShirtsQuery(int Skip = 0, int Take = 100) : IRequest<IEnumerable<ShirtDTO>>;

public class GetAllShirtQueryHandler : IRequestHandler<GetAllShirtsQuery, IEnumerable<ShirtDTO>>
{
    private readonly IShirtRepository _repository;
    private readonly IMapper _mapper;

    public GetAllShirtQueryHandler(IShirtRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShirtDTO>> Handle(GetAllShirtsQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<ShirtDTO>>(await _repository.FindAllAsync(request.Skip, request.Take));
}