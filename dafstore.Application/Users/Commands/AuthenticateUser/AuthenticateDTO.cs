namespace dafstore.Application.Users.Commands.AuthenticateUser;

public class AuthenticateDTO
{
    public AuthenticateDTO( string id, string name, string email, string[] roles)
    {
        Id = id;
        Name = name;
        Email = email;
        Roles = roles;
    }

    public string Token { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}