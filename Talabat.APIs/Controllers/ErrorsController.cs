using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers;
[Route("errors/{Code}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)] // Igonre Documentation for this Controller in Swagger 
public class ErrorsController : ControllerBase
{
    public ActionResult Error(int Code)
    {
        return NotFound(new ApiResponse(Code, "End Point is Not Found"));
    }
}
