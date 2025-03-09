using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Product = Talabat.Core.Entites.Product;

namespace Talabat.Services;
public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IConfiguration configuration,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
    {
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

        // Get Basket
        var Basket = await _basketRepository.GetBasketAsync(BasketId);
        if (Basket is null) return null;

        // Amount = SubTotal + DeliveryMethod Cost
        var ShippingPrice = 0M;
        if (Basket.DeliveryMethodId.HasValue)
        {
            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
            ShippingPrice = DeliveryMethod.Cost;
        }

        if (Basket.Items.Count > 0)
        {
            foreach (var item in Basket.Items)
            {
                var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                if (item.Price != Product.Price)
                    item.Price = Product.Price;
            }
        }

        var SubTotal = Basket.Items.Sum(item => item.Price * item.Quantity);

        var Service = new PaymentIntentService();

        PaymentIntent paymentIntent;
        if (string.IsNullOrEmpty(Basket.PaymentIntentId))  // Create
        {
            var Options = new PaymentIntentCreateOptions()
            {
                Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            paymentIntent = await Service.CreateAsync(Options);

            Basket.PaymentIntentId = paymentIntent.Id;
            Basket.ClientSecret = paymentIntent.ClientSecret;
        }

        else   // Update
        {
            var Options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100
            };

            paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, Options);

            Basket.PaymentIntentId = paymentIntent.Id;
            Basket.ClientSecret = paymentIntent.ClientSecret;
        }

        await _basketRepository.UpdateBasketAsync(Basket);
        return Basket;

    }

}
