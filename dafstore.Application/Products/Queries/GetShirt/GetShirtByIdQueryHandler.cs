using AutoMapper;
using dafstore.Application.Products.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Products.Queries.GetShirt;

public record GetShirtByIdQuery(Guid Id) : IRequest<ShirtDTO>;

public class GetShirtByIdQueryHandler : IRequestHandler<GetShirtByIdQuery, ShirtDTO>
{
    private readonly IShirtRepository _repository;
    private readonly IMapper _mapper;

    public GetShirtByIdQueryHandler(IShirtRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ShirtDTO> Handle(GetShirtByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<ShirtDTO>(await _repository.FindByIdAsync(request.Id));
}