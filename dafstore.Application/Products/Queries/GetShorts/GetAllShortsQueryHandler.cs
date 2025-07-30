using AutoMapper;
using dafstore.Application.Products.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Products.Queries.GetShorts;

public record GetAllShortsQuery(int Skip = 0, int Take = 100) : IRequest<IEnumerable<ShortsDTO>>;

public class GetAllShortsQueryHandler : IRequestHandler<GetAllShortsQuery, IEnumerable<ShortsDTO>>
{
    private readonly IShortsRepository _repository;
    private readonly IMapper _mapper;

    public GetAllShortsQueryHandler(IShortsRepository  repository,  IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ShortsDTO>> Handle(GetAllShortsQuery request, CancellationToken cancellationToken) => 
        _mapper.Map<IEnumerable<ShortsDTO>>(await _repository.FindAllAsync(request.Skip, request.Take));
}