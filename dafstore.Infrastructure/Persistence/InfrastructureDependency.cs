using dafstore.Application.Shared.Abstractions;
using dafstore.Infrastructure.Persistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dafstore.Infrastructure.Persistence;

public static class InfrastructureDependency
{
    public static void AddInfrastructure(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            options.UseNpgsql(connectionString, providerOptions =>
            {
                providerOptions.MigrationsHistoryTable("__ef_migrations_history");
                providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

            options.UseSnakeCaseNamingConvention();
        });
    }
}