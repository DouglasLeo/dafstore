using AutoMapper;
using dafstore.Application.Products.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Products.Queries.GetPants;

public record GetAllPantsQuery(int Skip = 0, int Take = 100) : IRequest<IEnumerable<PantsDTO>>;

public class GetAllPantsQueryHandler : IRequestHandler<GetAllPantsQuery, IEnumerable<PantsDTO>>
{
    private readonly IPantsRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPantsQueryHandler(IPantsRepository  repository,  IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PantsDTO>> Handle(GetAllPantsQuery request, CancellationToken cancellationToken) => 
        _mapper.Map<IEnumerable<PantsDTO>>(await _repository.FindAllAsync(request.Skip, request.Take));
}