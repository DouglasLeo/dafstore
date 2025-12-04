using dafstore.Domain.Contexts.UserContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Users.Configurations;

public class RoleConfiguration : EntityTypeConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.ToTable("roles");
        builder.HasKey(r => r.Id);
        builder.Property(u => u.Name).HasMaxLength(120).IsRequired();
    }
}