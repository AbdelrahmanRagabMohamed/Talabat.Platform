using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;
using Talabat.Services;

namespace Talabat.APIs.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection Services)  // Extension Method
    {

        /// Allow Dependcy Injection Non Generic
        ///builder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
        ///builder.Services.AddScoped<IGenericRepository<ProductBrand>,GenericRepository<ProductBrand>>();
        ///builder.Services.AddScoped<IGenericRepository<ProductType>,GenericRepository<ProductType>>();

        // Allow Dependcy Injection Generic
        Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Allow DI For All Repositories

        // builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
        Services.AddAutoMapper(typeof(MappingProfiles));

        // Allow Dependcy Injection
        Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

        // Allow Dependcy Injection
        Services.AddScoped<IUnitOfWork, UnitOfWork>();

        Services.AddScoped<IOrderService, OrderService>();

        Services.AddScoped<IPaymentService, PaymentService>();

        #region Error Handling

        // Validation Error Response - Configure 
        Services.Configure<ApiBehaviorOptions>(Options =>
         {
             Options.InvalidModelStateResponseFactory = actionContext =>
             {
                 // ModelState => Dicationary [KeyValuePair]
                 // Key => Name of Parameter
                 // Value => Error Message

                 var errors = actionContext.ModelState.Where(E => E.Value.Errors.Count > 0)
                 .SelectMany(E => E.Value.Errors)
                 .Select(E => E.ErrorMessage)
                 .ToArray();

                 var ValidationErrorResponse = new ApiValidationErrorResponse
                 {
                     Errors = errors
                 };

                 return new BadRequestObjectResult(ValidationErrorResponse);
             };
         });

        #endregion

        return Services;

    }
}
