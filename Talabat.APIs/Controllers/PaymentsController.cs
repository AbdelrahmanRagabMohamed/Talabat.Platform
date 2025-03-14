using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers;

public class PaymentsController : APIBaseController
{

    private readonly IPaymentService _paymentService;
    const string endpointSecret = "whsec_e8fa4f5b14f0f005aab5c1be380cc1abbb5434c134eb4155bbe18297777f0ac3";
    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // Create Or Update Payment Intent EndPoint => POST : baseUrl/api/Payment/basketId
    [HttpPost("{basketId}")]

    public async Task<ActionResult<CustomerBasketDto>> CreateORUpdatePaymentIntent(string basketId)
    {
        var Basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (Basket is null) return BadRequest(new ApiResponse(400, "There is Problem With Your Basket"));

        return Ok(Basket);
    }


    // EndPoint to make Stripe Sends Confirmation to API That Payment was Successful -  POST : baseUrl/api/Payments/webhook
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await _paymentService.UpdatePaymentIntentToSuccessOrFailed(paymentIntent.Id, false);
            }

            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await _paymentService.UpdatePaymentIntentToSuccessOrFailed(paymentIntent.Id, true);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            return BadRequest();
        }

    }
}
