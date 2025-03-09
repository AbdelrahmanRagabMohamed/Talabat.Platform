namespace Talabat.Core.Entites;
public class CustomerBasket
{
    public string Id { get; set; }

    public List<BasketItem> Items { get; set; }

    public CustomerBasket(string id)
    {
        Id = id;
    }

    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public int? DeliveryMethodId { get; set; }

}
