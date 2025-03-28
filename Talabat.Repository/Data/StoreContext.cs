﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Repository.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /// لوحدها entity لكل ApplyConfiguration الطريقة القديمة => ممكن نعمل 
        /// modelBuilder.ApplyConfiguration(new ProductConfigurations());   
        /// modelBuilder.ApplyConfiguration(new ProductBrandConfigurations());
        /// modelBuilder.ApplyConfiguration(new ProductTypeConfigurations());

        ///  مرة واحدة entites لكل  ApplyConfiguration الطريقة الجديدية هتعمل 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }



}
