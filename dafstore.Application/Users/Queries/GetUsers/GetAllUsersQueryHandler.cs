using AutoMapper;
using dafstore.Application.Users.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.Users.Queries.GetUsers;

public record GetAllUsersQuery(int Skip = 0, int Take = 100) : IRequest<IEnumerable<UserDTO>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<UserDTO>>(await _repository.FindAllAsync(request.Skip, request.Take));
}