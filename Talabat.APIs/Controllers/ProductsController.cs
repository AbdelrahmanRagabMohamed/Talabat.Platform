using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers;

public class ProductsController : APIBaseController
{
    private readonly Core.Repositories.IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper)
    {
        _productRepo = ProductRepo;
        _mapper = mapper;
    }


    // Get All Products Endpoint
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var Spec = new ProducWithBrandAndTypeSpecifications();
        var Products = await _productRepo.GetAllWithSpecAsync(Spec);

        // Mapping 
        var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToRetuenDto>>(Products);

        return Ok(MappedProducts);
    }


    // Get Product By Id Endpoint
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var Spec = new ProducWithBrandAndTypeSpecifications(id);
        var product = await _productRepo.GetByIdWithSpecAsync(Spec);

        if (product == null)
        {
            return NotFound(new ApiResponse(404));
        }

        // Mapping
        var MappedProduct = _mapper.Map<Product, ProductToRetuenDto>(product);

        return Ok(MappedProduct);
    }

}
