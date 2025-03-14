using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers;
public class BasketController : APIBaseController
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
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
    public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasketDto customerBasket)
    {
        /// Update Basket In Redis
        /// If Basket Not Exist => Create New Basket
        /// Else Update Basket

        var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);

        var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(MappedBasket);

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
