using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Domain.Contexts.UserContext.Entities;
using dafstore.Domain.Contexts.UserContext.ValueObjects;

namespace dafstore.Application.Users.Abstractions.Repository;

public interface IUserRepository :  IRepository<User>
{
    Task<User?> FindUserByEmail(Email email);
    Task<User?> FindUserByEmailIncludeRoles(Email email);
    Task<User?> FindUserByPhone(Phone phone);
}