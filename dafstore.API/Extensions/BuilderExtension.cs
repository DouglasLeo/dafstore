using dafstore.Domain.Shared;

namespace dafstore.API.Extensions;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        
        Configuration.Secrets.JwtPrivateKey =
            builder.Configuration.GetSection("JwtPrivateKey").Value ?? string.Empty;
    }
}