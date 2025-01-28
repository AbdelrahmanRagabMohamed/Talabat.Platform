using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data.Configurations;
internal class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(T => T.Name).IsRequired();

    }
}
