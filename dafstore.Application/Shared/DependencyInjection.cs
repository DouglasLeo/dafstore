using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace dafstore.Application.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(x => x.RegisterServicesFromAssembly(assembly))
            .AddAutoMapper(assembly);

        return services;
    }
}