using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers;

public class PaymentController : APIBaseController
{

    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // Create Or Update Payment Intent EndPoint => POST : baseUrl/api/Payment/basketId
    [HttpPost]
    public async Task<ActionResult<CustomerBasketDto>> CreateORUpdatePaymentIntent(string basketId)
    {
        var Basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (Basket is null) return BadRequest(new ApiResponse(400, "There is Problem With Your Basket"));

        return Ok(Basket);
    }
}
