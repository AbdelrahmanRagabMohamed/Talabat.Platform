using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_Spec;
public class OrderWithPaymentIntentSpec : BaseSpecifications<Order>
{
    public OrderWithPaymentIntentSpec(string PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId)
    {

    }
}
