using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers;
public class BasketsController : APIBaseController
{
    private readonly IBasketRepository _basketRepository;

    public BasketsController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }


    // Endpint To GET or ReCreate Basket
    [HttpGet("{BasketId}")]
    public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
    {
        /// Get Basket From Redis
        /// If Basket Not Exist => Create New Basket
        /// Else Return Basket

        var Basket = await _basketRepository.GetBasketAsync(BasketId);

        return Basket is null ? new CustomerBasket(BasketId) : Ok(Basket);
    }


    // Endpint To Update OR Create New Basket 
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasket basket)
    {
        /// Update Basket In Redis
        /// If Basket Not Exist => Create New Basket
        /// Else Update Basket

        var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);

        if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));

        return Ok(CreatedOrUpdatedBasket);


    }


    // Endpint To Delete Basket
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCustomerBasket(string BasketId)
    {
        /// Delete Basket From Redis
        return await _basketRepository.DeleteBasketAsync(BasketId);
    }


}
