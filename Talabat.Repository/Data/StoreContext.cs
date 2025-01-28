using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Repository.Data.Configurations;

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
}
