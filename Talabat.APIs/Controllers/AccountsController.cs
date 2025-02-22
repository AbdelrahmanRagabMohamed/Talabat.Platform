using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers;

public class AccountsController : APIBaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountsController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }


    // Register EndPoint => POST:  BaseUrl/api/Accounts/Register
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto model)
    {

        // Manual Mapping (Mapping From RegisterDto To AppUser Because => AppUser Deal With Db)
        var User = new AppUser()
        {
            DisplayName = model.DisplayName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            UserName = model.Email.Split('@')[0]
        };

        var Result = await _userManager.CreateAsync(User, model.Password);

        if (!Result.Succeeded) return BadRequest(Result.Errors);

        // Manual Mapping (Mapping From AppUser To UserDto Because => UserDto is the Type of returned Data)
        var ReturnedUser = new UserDto()
        {
            // model = User
            DisplayName = User.DisplayName,
            Email = User.Email,
            Token = await _tokenService.CreateTokenAsync(User, _userManager)
        };

        return Ok(ReturnedUser);
    }


    // Login EndPoint => POST: BaseUrl/api/Accounts/Login
    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto model)
    {
        var User = await _userManager.FindByEmailAsync(model.Email); // Find User By Email

        if (User is null) // If User Not Found
            return Unauthorized(new ApiResponse((401)));

        var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);   // Check Password

        if (!Result.Succeeded)  // If Password is Incorrect
            return Unauthorized(new ApiResponse(401));

        /// Else => Return User Data
        /// Manual Mapping (Mapping From AppUser To UserDto Because => UserDto is the Type of returned Data)

        return Ok(new UserDto()
        {
            // model = User
            Email = User.Email,
            DisplayName = User.DisplayName,
            Token = await _tokenService.CreateTokenAsync(User, _userManager)
        });

    }

}
