using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Products.Configurations;

public class ShirtConfiguration  : EntityTypeConfiguration<Shirt>
{
    public override void Configure(EntityTypeBuilder<Shirt> builder)
    {
        builder.Property(s => s.ShirtCategory).IsRequired();
    }
}