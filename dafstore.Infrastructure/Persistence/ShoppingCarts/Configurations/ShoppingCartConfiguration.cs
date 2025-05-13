using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.ShoppingCarts.Configurations;

public class ShoppingCartConfiguration : EntityTypeConfiguration<ShoppingCart>
{
    public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        base.Configure(builder);

        builder.HasMany(s => s.ShoppingCartItems).WithOne().HasForeignKey(i => i.ShoppingCartId);
    }
}