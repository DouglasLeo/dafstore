using System.Text;
using dafstore.Application.Orders.Abstractions.Repositories;
using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Application.Shared.Abstractions;
using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Application.ShoppingCarts.Abstractions.Repository;
using dafstore.Application.Users.Abstractions.Repository;
using dafstore.Application.Users.Abstractions.Services;
using dafstore.Domain.Shared;
using dafstore.Infrastructure.Persistence.Orders.Repositories;
using dafstore.Infrastructure.Persistence.Products.Repositories;
using dafstore.Infrastructure.Persistence.Shared;
using dafstore.Infrastructure.Persistence.Shared.Repositories;
using dafstore.Infrastructure.Persistence.ShoppingCarts.Repositories;
using dafstore.Infrastructure.Persistence.Users.Repositories;
using dafstore.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace dafstore.Infrastructure.DependencyInjection;

public static class InfrastructureDependency
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Configuration.Database.ConnectionString, providerOptions =>
            {
                providerOptions.MigrationsHistoryTable("__ef_migrations_history");
                providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPantsRepository, PantsRepository>();
        services.AddScoped<IShirtRepository, ShirtRepository>();
        services.AddScoped<IShortsRepository, ShortsRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IAuthenticateService, AuthenticateService>();
    }

    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization();
    }
}