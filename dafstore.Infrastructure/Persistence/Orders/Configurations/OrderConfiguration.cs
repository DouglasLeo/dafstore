using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dafstore.Infrastructure.Persistence.Orders.Configurations;

public class OrderConfiguration : EntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        
        builder.HasKey(s => s.Id);
        builder.Property(s => s.OrderDate).IsRequired();
        builder.Property(s => s.OrderStatus).IsRequired();
        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.PaymentMethod).IsRequired();
        builder.Property(s => s.Total).IsRequired();
        
        builder.OwnsMany(o => o.OrderItems);
        
        builder.OwnsOne(u => u.DeliveryAddress, address =>
        {
            address.Property(s => s.CustomerName).HasMaxLength(150).IsRequired();
            address.Property(a => a.Street).HasMaxLength(200).IsRequired();
            address.Property(a => a.Number).HasMaxLength(50).IsRequired();
            address.Property(a => a.State).HasMaxLength(200).IsRequired();
            address.Property(a => a.City).HasMaxLength(200).IsRequired();
            address.Property(a => a.ZipCode).HasMaxLength(256).IsRequired();
            address.Property(a => a.Country).HasMaxLength(256).IsRequired();
        });
    }
}