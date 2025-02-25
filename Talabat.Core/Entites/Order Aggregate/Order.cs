namespace Talabat.Core.Entites.Order_Aggregate;
public class Order : BaseEntity
{
    public Order()
    {

    }

    public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
    {
        BuyerEmail = buyerEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        Items = items;
        SubTotal = subTotal;
    }

    public string BuyerEmail { get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public Address ShippingAddress { get; set; }

    // public int DeliveryMethodId { get; set; }    // FK
    public DeliveryMethod DeliveryMethod { get; set; }

    // Navigational Property [Many]
    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

    public decimal SubTotal { get; set; }     // Product Price * Quantity


    //[NotMapped]  // Not Mapped in DB
    //public decimal Total { get => SubTotal + DeliveryMethod.Cost; }
    public decimal Total()
       => SubTotal + DeliveryMethod.Cost;

    public string PaymentIntentId { get; set; } = string.Empty;  // مؤقتا

}
