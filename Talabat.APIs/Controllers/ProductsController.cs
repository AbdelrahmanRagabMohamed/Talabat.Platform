using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    
}
