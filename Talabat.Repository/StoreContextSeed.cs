using System.Text.Json;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Repository.Data;

namespace Talabat.Repository;
public static class StoreContextSeed
{
    // Seeding

    public static async Task SeedAsync(StoreContext dbContext)
    {
        // لو مفيش داتا دخلت قبل كده و لو في مش هينفذها data seeding هيعمل 

        if (!dbContext.ProductBrands.Any())
        {

            // Seeding Brands
            var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json"); //convert json data to string [Serializer]
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData); // convert string data to json [Deserializer]

            if (Brands?.Count > 0)
            {
                foreach (var brand in Brands)
                    await dbContext.Set<ProductBrand>().AddAsync(brand);
            }
        }

        if (!dbContext.ProductTypes.Any())
        {

            // Seeding Types
            var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
            var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

            if (Types?.Count > 0)
            {
                foreach (var type in Types)
                    await dbContext.Set<ProductType>().AddAsync(type);
            }
        }

        if (!dbContext.Products.Any())
        {
            // Seeding Products
            var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

            if (Products?.Count > 0)
            {
                foreach (var product in Products)
                    await dbContext.Set<Product>().AddAsync(product);
            }
        }


        if (!dbContext.DeliveryMethods.Any())
        {
            // Seeding DeliveryMethods
            var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
            var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

            if (DeliveryMethods?.Count > 0)
            {
                foreach (var DeliveryMethod in DeliveryMethods)
                    await dbContext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
            }
        }


        await dbContext.SaveChangesAsync();

    }
}
