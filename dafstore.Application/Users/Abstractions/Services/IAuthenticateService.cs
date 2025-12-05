using dafstore.Application.Users.Commands.AuthenticateUser;

namespace dafstore.Application.Users.Abstractions.Services;

public interface IAuthenticateService
{
    string Generate(AuthenticateDTO dto);
}