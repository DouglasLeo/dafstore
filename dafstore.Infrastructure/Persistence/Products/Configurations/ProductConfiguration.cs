using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Products.Configurations;

public class ProductConfiguration : EntityTypeConfiguration<Product>
{
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
                base.Configure(builder);
                
                builder.Property(s => s.StockId).IsRequired();
                builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
                builder.Property(s => s.Description).HasMaxLength(500);
                builder.Property(s => s.Price).IsRequired();

                builder.Property(s => s.Size).IsRequired();
                builder.Property(s => s.Colors).IsRequired();
                
                builder.Property(s => s.ImageKeys).IsRequired();

                builder.HasMany(p => p.Category).WithMany();
        }
}