using dafstore.Application.Users.Abstractions.Repository;
using dafstore.Domain.Contexts.UserContext.Entities;
using dafstore.Domain.Contexts.UserContext.ValueObjects;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace dafstore.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Street,
    string Number,
    string City,
    string State,
    string ZipCode,
    string Country,
    string Phone)
    : IRequest<Guid>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        var address = new Address(request.Street, request.Number, request.City, request.State, request.ZipCode,
            request.Country, true);

        var passwordHash = BC.HashPassword(request.Password);

        var user = new User(new UserName(request.FirstName, request.LastName), request.Email, passwordHash, address,
            new Phone(request.Phone));

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        return user.Id;
    }
}