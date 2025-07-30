using AutoMapper;
using dafstore.Application.Users.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.Users.Queries.GetUsers;

public record GetUserByEmailQuery(string Email) : IRequest<UserDTO>;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDTO>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<UserDTO>(await _repository.FindUserByEmail(request.Email));
}