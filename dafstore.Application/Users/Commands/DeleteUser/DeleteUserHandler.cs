using dafstore.Application.Users.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<Guid>;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _repository.FindByIdAsync(request.Id);

        if (userToUpdate is null) return Guid.Empty;

        await _repository.RemoveAsync(userToUpdate);
        await _repository.SaveChangesAsync();

        return userToUpdate.Id;
    }
}