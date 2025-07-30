using dafstore.Application.Users.Abstractions.Repository;
using dafstore.Domain.Contexts.UserContext.ValueObjects;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace dafstore.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, string FirstName, string LastName, string Email, string Password, string Phone)
    : IRequest<Guid>;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _repository.FindByIdAsync(request.Id);

        if (userToUpdate is null) return Guid.Empty;

        userToUpdate.UpdateName(new UserName(request.FirstName, request.LastName));
        userToUpdate.UpdateEmail(request.Email);
        userToUpdate.UpdatePassword(BC.HashPassword(request.Password));
        userToUpdate.UpdatePhone(request.Phone);

        await _repository.UpdateAsync(userToUpdate);
        await _repository.SaveChangesAsync();

        return userToUpdate.Id;
    }
}