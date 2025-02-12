
namespace Talabat.APIs.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }


    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(StatusCode);
    }

    private string? GetDefaultMessage(int statusCode)
    {
        /// 400 => Bad Request
        /// 401 => Unauthorized
        /// 404 => Not Found 
        /// 500 => Internal Server Error

        return StatusCode switch
        {
            400 => "Bad Request",
            401 => "You Are Not Authorized",
            404 => "Resource Not Found ",
            500 => "Internal Server Error",

            _ => null     // _ => Default Value

        };

    }
}
