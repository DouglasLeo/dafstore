using AutoMapper;
using dafstore.Application.Products.Abstractions.Repositories;
using MediatR;

namespace dafstore.Application.Products.Queries.GetPants;

public record GetPantsByIdQuery(Guid Id) : IRequest<PantsDTO>;

public class GetPantsByIdQueryHandler : IRequestHandler<GetPantsByIdQuery, PantsDTO>
{
    private readonly IPantsRepository _repository;
    private readonly IMapper _mapper;

    public GetPantsByIdQueryHandler(IPantsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PantsDTO> Handle(GetPantsByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<PantsDTO>(await _repository.FindByIdAsync(request.Id));
}