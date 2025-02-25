using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations;
public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(DM => DM.Cost)
               .HasColumnType("decimal(18,2)");
    }
}
