using AutoMapper;
using dafstore.Application.Users.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.Users.Queries.GetUsers;

public record GetUserByPhoneQuery(string Phone) : IRequest<UserDTO>;

public class GetUserByPhoneQueryHandler : IRequestHandler<GetUserByPhoneQuery, UserDTO>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByPhoneQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(GetUserByPhoneQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<UserDTO>(await _repository.FindUserByPhone(request.Phone));
}