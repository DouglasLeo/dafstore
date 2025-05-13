using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Products.Configurations;

public class PantsConfiguration  : EntityTypeConfiguration<Pants>
{
    public override void Configure(EntityTypeBuilder<Pants> builder)
    {
        builder.Property(p => p.TissueType).IsRequired();
    }
}