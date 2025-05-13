using dafstore.Application.Users.Abstractions.Repository;
using dafstore.Domain.Contexts.UserContext.Entities;
using dafstore.Domain.Contexts.UserContext.ValueObjects;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dafstore.Infrastructure.Persistence.Users.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> FindUserByEmail(Email email) =>
        await DbSet.SingleAsync(u => u.Email == email);

    public async Task<User?> FindUserByPhone(Phone phone) =>
        await DbSet.SingleAsync(u => u.Phone == phone);
}