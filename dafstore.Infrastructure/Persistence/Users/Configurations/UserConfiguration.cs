using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Domain.Contexts.UserContext.Entities;
using dafstore.Domain.Contexts.UserContext.ValueObjects;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Users.Configurations;

public class UserConfiguration : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.Property(u => u.Password).IsRequired();

        builder.HasOne(u => u.ShoppingCart).WithOne().HasForeignKey<ShoppingCart>(s => s.UserId).IsRequired();
        builder.HasMany(u => u.Orders).WithOne().HasForeignKey(o => o.UserId).IsRequired();
        builder.OwnsMany(u => u.Addresses, address =>
        {
            address.Property(a => a.Street).HasMaxLength(200).IsRequired();
            address.Property(a => a.Number).HasMaxLength(50).IsRequired();
            address.Property(a => a.State).HasMaxLength(200).IsRequired();
            address.Property(a => a.City).HasMaxLength(200).IsRequired();
            address.Property(a => a.ZipCode).HasMaxLength(256).IsRequired();
            address.Property(a => a.Country).HasMaxLength(256).IsRequired();
            address.Property(a => a.Active).IsRequired();
        });
        
        builder.ComplexProperty(u => u.UserName).Property(name => name.FirstName).HasColumnName("first_name")
            .HasMaxLength(60).IsRequired();
        builder.ComplexProperty(u => u.UserName).Property(name => name.LastName).HasColumnName("last_name")
            .HasMaxLength(200).IsRequired();

        builder.OwnsOne(u => u.Email, buildAction =>
        {
            buildAction.Property(e => e.EmailAddress).HasColumnName("email_address").HasMaxLength(256).IsRequired();
            buildAction.HasIndex(e => e.EmailAddress, "IX_User_Email").IsUnique();
        });
        
        builder.OwnsOne(u => u.Phone, buildAction =>
        {
            buildAction.Property(p => p.PhoneNumber).HasColumnName("email_address").HasColumnName("phone_number").HasMaxLength(100).IsRequired();
            buildAction.HasIndex(p => p.PhoneNumber, "IX_User_Phone").IsUnique();
        });
    }
}