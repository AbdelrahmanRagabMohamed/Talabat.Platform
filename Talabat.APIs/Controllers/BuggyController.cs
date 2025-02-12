using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers;

public class BuggyController : APIBaseController
{
    private readonly StoreContext _dbcontext;

    public BuggyController(StoreContext dbcontext)
    {
        _dbcontext = dbcontext;
    }


    [HttpGet("NotFound")]     // BaseUrl/api/Buggy/NotFound
    public ActionResult GetNotFoundRequest()
    {
        var Product = _dbcontext.Products.Find(122); // this product not found (null)
        if (Product is null)
        {
            return NotFound(new ApiResponse(404));
        }
        else
            return Ok(Product);
    }


    [HttpGet("ServerError")]
    public ActionResult GetServerError()
    {
        var Product = _dbcontext.Products.Find(122);

        // Error (Can not convert null to String)
        var ProductToReturn = Product.ToString();

        // Will Throw Exception [Null Reference Exception] 

        return Ok(ProductToReturn);
    }


    [HttpGet("BadRequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest();
    }


    [HttpGet("BadRequest/{id}")]
    public ActionResult GetServerError(int id)
    {
        return Ok();
    }

}
