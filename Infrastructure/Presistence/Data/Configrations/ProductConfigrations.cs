using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configrations
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p=>p.ProductType)
                .WithMany().
                HasForeignKey(x => x.TypeId) ;

            builder.HasOne(p=>p.ProductBrand)
                .WithMany().HasForeignKey(x => x.BrandId) ;

            builder.Property(p => p.Price)
                .HasColumnType("decimal(14,2)");
            
        }
    }
}
