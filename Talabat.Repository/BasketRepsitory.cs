using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.Repository;
public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    // Ask CLR To Create object from Class that Implement Interface IConnectionMultiplexer => To Deal with Redis. 
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }


    public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
    {
        var Basket = await _database.StringGetAsync(BasketId);

        return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
    {
        var JsonBaket = JsonSerializer.Serialize(Basket);

        // Id بنفس ال  Create و لو مش موجوده هيعملها Update لو موجوده هيعملها   
        var CreatedOrUpdated = await _database.StringSetAsync(Basket.Id, JsonBaket, TimeSpan.FromDays(1));

        if (!CreatedOrUpdated) return null;

        return await GetBasketAsync(Basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string BasketId)
    {
        return await _database.KeyDeleteAsync(BasketId);
    }

}
