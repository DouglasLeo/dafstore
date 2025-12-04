using AutoMapper;
using dafstore.Application.Products.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Products.Queries.GetShorts;

public record GetShortsByIdQuery(Guid Id) : IRequest<ShortsDTO>;

public class GetShortsByIdQueryHandler : IRequestHandler<GetShortsByIdQuery, ShortsDTO>
{
    private readonly IShortsRepository _repository;
    private readonly IMapper _mapper;

    public GetShortsByIdQueryHandler(IShortsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ShortsDTO> Handle(GetShortsByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<ShortsDTO>(await _repository.FindByIdAsync(request.Id));
}