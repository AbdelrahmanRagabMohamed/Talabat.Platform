using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers;

public class ProductsController : APIBaseController
{
    private readonly Core.Repositories.IGenericRepository<Product> _productRepo;

    public ProductsController(IGenericRepository<Product> ProductRepo)
    {
        _productRepo = ProductRepo;
    }


    // Get All Products Endpoint
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
    {
        var Products = await _productRepo.GetAllAsync();

        //OkObjectResult products = new OkObjectResult(Products);
        //return products;
        return Ok(Products);
    }   


    // Get Product By Id Endpoint
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        return Ok(product);
    }     




}
