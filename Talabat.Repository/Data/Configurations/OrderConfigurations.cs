using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entites.Order_Aggregate;


namespace Talabat.Repository.Data.Configurations;
public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(OStatus => OStatus.Status)
               .HasConversion(OStatus => OStatus.ToString(), OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));

        builder.Property(O => O.SubTotal)
               .HasColumnType("decimal(18,2)");

        builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());

        builder.HasOne(O => O.DeliveryMethod)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
    }
}
