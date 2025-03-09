using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data.Configurations;
internal class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(P => P.ProductBrand)
            .WithMany()
            .HasForeignKey(P => P.ProductBrandId);  /// FK مش محتاجة عشان مغيرتش في ال 


        builder.HasOne(P => P.ProductType)
            .WithMany()
            .HasForeignKey("ProductTypeId");  /// FK مش محتاجة عشان مغيرتش في ال 


        builder.Property(P => P.Name).IsRequired().HasMaxLength(50);
        builder.Property(P => P.Description).IsRequired();
        builder.Property(P => P.PictureUrl).IsRequired();
        builder.Property(P => P.Price).HasColumnType("decimal(18,2)");






    }
}
