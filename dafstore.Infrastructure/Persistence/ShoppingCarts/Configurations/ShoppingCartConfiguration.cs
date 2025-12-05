using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.ShoppingCarts.Configurations;

public class ShoppingCartConfiguration : EntityTypeConfiguration<ShoppingCart>
{
    public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        base.Configure(builder);
        
        builder
            .HasMany(c => c.ShoppingCartItems)
            .WithOne(c => c.ShoppingCart) 
            .HasForeignKey(i => i.ShoppingCartId)
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(ShoppingCart.ShoppingCartItems))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}