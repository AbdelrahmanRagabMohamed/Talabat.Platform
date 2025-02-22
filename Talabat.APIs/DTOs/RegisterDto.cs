using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+]).{6,10}$",
        ErrorMessage = "Password must contain 1 Uppercase, 1 Lowercase, 1 Digit, 1 Special Character")]
    public string Password { get; set; }
}
