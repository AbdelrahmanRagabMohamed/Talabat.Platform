using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers;

public class AccountsController : APIBaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountsController
        (UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IMapper mapper)

    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }


    // Register EndPoint => POST:  BaseUrl/api/Accounts/Register
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto model)
    {

        // Works Synchronous [Stop all until Check]
        if (CheckEmailExsits(model.Email).Result.Value)
        {
            return BadRequest(new ApiResponse(400, "Email is already in use"));
        }

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


    // GET Current User EndPoint => GET : BaseUrl/api/Accounts/GetCurrentUser
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("GetCurrentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var Email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(Email);

        var ReturnedUser = new UserDto()
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _tokenService.CreateTokenAsync(user, _userManager)
        };

        return Ok(ReturnedUser);
    }


    // GET Current User Address EndPoint => GET : BaseUrl/api/Accounts/UserAdress
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("UserAdress")]
    public async Task<ActionResult<AddressDto>> GetUserAdress()
    {
        //var Email = User.FindFirstValue(ClaimTypes.Email);
        //var user = await _userManager.FindByEmailAsync(Email);     ==> Invalid

        var user = await _userManager.FindUserWithAddressAsync(User);

        // using Auto Mapper
        var MappedUser = _mapper.Map<Address, AddressDto>(user.Address);

        return Ok(MappedUser);
    }


    // Update Current User Address EndPoint => PUT : BaseUrl/api/Accounts/UserAdress
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("UserAdress")]
    public async Task<ActionResult<AddressDto>> UpdateUserAdress(AddressDto UpdatedAdress)
    {
        var user = await _userManager.FindUserWithAddressAsync(User);

        var MappedAdress = _mapper.Map<AddressDto, Address>(UpdatedAdress);

        MappedAdress.Id = user.Address.Id;  // Update User Address for same Id (for same user) Not Inser New Adress 

        user.Address = MappedAdress;

        var Result = await _userManager.UpdateAsync(user);

        if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

        return Ok(Result);


    }


    // Check if Email Exsits EndPoint => GET : BaseUrl/api/Accounts/emailExsits
    [HttpGet("emailExsits")]
    public async Task<ActionResult<bool>> CheckEmailExsits(string Email)
    {

        ///var User = await _userManager.FindByEmailAsync(Email);
        ///if (User is null) return false;
        ///else return true;

        return await _userManager.FindByEmailAsync(Email) is not null;

    }


}

