using dafstore.Application.Users.Abstractions.Repository;
using dafstore.Application.Users.Abstractions.Services;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace dafstore.Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string Email, string Password) : IRequest<AuthenticateDTO>;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateDTO?>
{
    private readonly IUserRepository _repository;
    private readonly IAuthenticateService _authenticateService;

    public AuthenticateUserHandler(IUserRepository repository, IAuthenticateService  authenticateService)
    {
        _repository = repository;
        _authenticateService = authenticateService;
    }
    
    public async Task<AuthenticateDTO?> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var  user = await _repository.FindUserByEmailIncludeRoles(request.Email);
        
        if (user == null) return null;

        var match = BC.Verify(request.Password, user.Password);
        
        if(!match) return null;
        
        var response = new AuthenticateDTO(user.Id.ToString(), user.UserName.ToString(), user.Email, user.Roles.Select(r => r.Name).ToArray());
        response.Token = _authenticateService.Generate(response);
        
        return response;
    }
}