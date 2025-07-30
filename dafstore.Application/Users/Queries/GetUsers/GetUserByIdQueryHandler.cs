using AutoMapper;
using dafstore.Application.Users.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.Users.Queries.GetUsers;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDTO>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<UserDTO>(await _repository.FindByIdAsync(request.Id));
}