using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x =>x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x => x.PictureUrl).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(b=>b.BrandId);
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(b=>b.CategoryId);

        }
    }
}
