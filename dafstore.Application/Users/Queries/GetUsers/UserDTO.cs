using AutoMapper;
using dafstore.Domain.Contexts.UserContext.Entities;

namespace dafstore.Application.Users.Queries.GetUsers;

public class UserDTO()
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Phone { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDTO>();
        }
    }
}