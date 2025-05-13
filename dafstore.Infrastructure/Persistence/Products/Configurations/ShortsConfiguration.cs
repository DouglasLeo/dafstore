using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Products.Configurations;

public class ShortsConfiguration : EntityTypeConfiguration<Shorts>
{
    public override void Configure(EntityTypeBuilder<Shorts> builder)
    {
        builder.Property(s => s.ShortsTissueType).IsRequired();
    }
}