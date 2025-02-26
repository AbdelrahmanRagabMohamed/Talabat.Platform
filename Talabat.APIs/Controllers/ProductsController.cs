using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers;

public class ProductsController : APIBaseController
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ProductType> _typeRepo;
    private readonly IGenericRepository<ProductBrand> _brandRepo;

    public ProductsController(IGenericRepository<Product> ProductRepo
        , IMapper mapper
        , IGenericRepository<ProductType> TypeRepo
        , IGenericRepository<ProductBrand> BrandRepo)
    {
        _productRepo = ProductRepo;
        _mapper = mapper;
        _typeRepo = TypeRepo;
        _brandRepo = BrandRepo;
    }



    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize] // فيها مشكلة 
    // Get All Products Endpoint
    [HttpGet]  // BaseUrl/api/Products
    public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery] ProductSpecParams Params)
    {
        var Spec = new ProducWithBrandAndTypeSpecifications(Params);
        var Products = await _productRepo.GetAllWithSpecAsync(Spec);

        // Mapping 
        var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

        // CountSpec => (Criteria)
        var CountSpec = new ProductWithFilterationForCountAsync(Params);
        var Count = await _productRepo.GetCountWithSpecAsync(CountSpec);

        // Using Pagination Standard Response
        var ReturnedProducts = new Pagination<ProductToReturnDto>()
        {
            PageSize = Params.PageSize,
            PageIndex = Params.PageIndex,
            Data = MappedProducts,
            Count = Count
        };

        return Ok(ReturnedProducts);

    }


    // Get Product By Id Endpoint
    [HttpGet("{id}")]  // BaseUrl/api/Products/{id}
    [ProducesResponseType(typeof(ProductToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var Spec = new ProducWithBrandAndTypeSpecifications(id);
        var product = await _productRepo.GetByIdWithSpecAsync(Spec);

        if (product == null)
        {
            return NotFound(new ApiResponse(404));
        }

        // Mapping
        var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);

        return Ok(MappedProduct);
    }


    // Get All Types Endpoint
    [HttpGet("Types")]  // BaseUrl/api/Products/Types
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
    {
        var Types = await _typeRepo.GetAllAsync();
        return Ok(Types);
    }


    // Get All Brands Endpoint
    [HttpGet("Brands")]  // BaseUrl/api/Products/Brands
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        var Brands = await _brandRepo.GetAllAsync();
        return Ok(Brands);
    }


}
