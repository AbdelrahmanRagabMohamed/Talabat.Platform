using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers;

public class OrdersController : APIBaseController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrdersController(IOrderService orderService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderService = orderService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    // Create Order EndPoint => POST : BaseURL/api/Orders
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
        var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

        var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);

        if (Order == null) return BadRequest(new ApiResponse(400, "There is a Problem With Your Order"));
        return Ok(Order);

    }


    // Get Orders For Specific User EndPoint => GET : BaseURL/api/Orders
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

        var Orders = await _orderService.GetOrdersForSpecificUserAsync(BuyerEmail);

        if (Orders is null) return NotFound(new ApiResponse(404, "There is No Orders For This User"));

        var MappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);

        return Ok(MappedOrders);
    }


    // Get Order By Id For Specific User EndPoint => GET : BaseURL/api/Orders
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
    {
        var BuyerEmail = User.FindFirstValue(claimType: ClaimTypes.Email);

        var Order = await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);

        if (Order is null) return NotFound(new ApiResponse(404, $"There is no Order with This Id:{id} For This User"));

        var MappedOrder = _mapper.Map<Order, OrderToReturnDto>(Order);

        return Ok(MappedOrder);
    }


    // Get All Delivery Methods EndPoint => GET : BaseURL/api/Orders/DeliveryMethods
    [HttpGet("DeliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        return Ok(DeliveryMethods);
    }




}


