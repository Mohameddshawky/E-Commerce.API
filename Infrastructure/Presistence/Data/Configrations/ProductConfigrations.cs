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
            builder.HasOne<ProductType>()
                .WithMany().
                HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne<ProductBrand>()
                .WithMany().HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.NoAction); ;

            builder.Property(p => p.Price)
                .HasColumnType("decimal(14,2)");
            
        }
    }
}
