using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Module.Users.Domain;
using Shared.Models;
using WebApi.Models.Auth;

namespace WebApi.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IOptionsMonitor<BearerTokenOptions> _bearerTokenOptions;
    private readonly TimeProvider _timeProvider;
    public AuthController(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IOptionsMonitor<BearerTokenOptions> bearerTokenOptions,
        TimeProvider timeProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _bearerTokenOptions = bearerTokenOptions;
        _timeProvider = timeProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(request);
        }

        var createdUser = await _userManager.FindByEmailAsync(user.Email);
        if (createdUser is null)
        {
            return BadRequest(request);
        }

        await _userManager.AddToRoleAsync(createdUser, RoleEnum.User.GetDisplayName());

        return await Login(new LoginRequestModel
        {
            Email = request.Email,
            Password = request.Password
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, lockoutOnFailure: true);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        return Empty;
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
    {
        var refreshTokenProtector = _bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
        var refreshTicket = refreshTokenProtector.Unprotect(request.RefreshToken);

        if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc
            || _timeProvider.GetUtcNow() >= expiresUtc
            || await _signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not User user)
        {
            return BadRequest();
        }

        var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        return SignIn(userPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
    }
}