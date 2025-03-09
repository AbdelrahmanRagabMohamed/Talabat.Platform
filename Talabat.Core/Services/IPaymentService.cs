using Talabat.Core.Entites;

namespace Talabat.Core.Services;
public interface IPaymentService
{
    Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId);
}
