using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Services;
public interface IPaymentService
{
    Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId);

    Task<Order> UpdatePaymentIntentToSuccessOrFailed(string PaymentIntentId, bool flag);
}
