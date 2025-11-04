using Domain.Entites.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configrations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, a => a.WithOwner());

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,4)");

            builder.HasMany(o => o.orderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p=>p.PaymentStatus)
                .HasConversion(p=>p.ToString(),rp=>Enum.Parse<OrderPaymentStatus>(rp));  
            
            builder.HasOne(o=>o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o=>o.DeliveryMethodID)
                .OnDelete(DeleteBehavior.SetNull);
             

        }
    }
}
