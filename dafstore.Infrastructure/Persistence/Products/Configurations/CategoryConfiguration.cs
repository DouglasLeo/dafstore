using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Products.Configurations;

public class CategoryConfiguration : EntityTypeConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        
        builder.ComplexProperty(s => s.Name).Property(c => c.Name).HasMaxLength(156).IsRequired();
        builder.Property(s => s.Description).HasMaxLength(500);
    }
}