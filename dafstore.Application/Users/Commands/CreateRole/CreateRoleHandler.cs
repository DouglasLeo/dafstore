using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Domain.Contexts.UserContext.Entities;
using MediatR;

namespace dafstore.Application.Users.Commands.CreateRole;

public record CreateRoleCommand(string Name)
    : IRequest<Guid>;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IRepository<Role> _repository;

    public CreateRoleHandler(IRepository<Role> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken = default)
    {
        var role = new Role(request.Name);

        await _repository.AddAsync(role);
        await _repository.SaveChangesAsync();

        return role.Id;
    }
}