using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.APIs.DTOs;

public class OrderToReturnDto
{
    public string BuyerEmail { get; set; }

    public DateTimeOffset OrderDate { get; set; }

    public string Status { get; set; }

    public Address ShippingAddress { get; set; }

    public string DeliveryMethod { get; set; }
    public decimal DeliveryMethodCost { get; set; }

    public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

    public decimal SubTotal { get; set; }

    public decimal Total { get; set; }

    public string PaymentIntentId { get; set; }
}
