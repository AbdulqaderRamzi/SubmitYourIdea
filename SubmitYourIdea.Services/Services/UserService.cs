using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SubmitYourIdea.ApiModels;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.DataAccess;
using SubmitYourIdea.DataAccess.Entities;
using SubmitYourIdea.Services.Interfaces;
using SubmitYourIdea.Services.Mappings;
using SubmitYourIdea.Services.Errors;

namespace SubmitYourIdea.Services.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ApplicationDbContext _db;

    public UserService(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenGenerator jwtTokenGenerator,
        ApplicationDbContext db) {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _db = db;
    }

    public async Task<ErrorOr<AuthenticationResponse>> RegisterAsync(RegisterRequest request)
    {
        var user = request.ToAppUser();
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return AuthenticationErrors.RegistrationFailed(
                result.Errors.First().Description);
        }

        if (!await _roleManager.RoleExistsAsync(Roles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await _roleManager.CreateAsync(new IdentityRole(Roles.Visitor));
        }
        await _userManager.AddToRoleAsync(user, Roles.Visitor); // example

        var accessToken = _jwtTokenGenerator.GetAccessToken(user, Roles.Visitor);

        return user.ToAuthResponse(accessToken, Roles.Visitor);

    }

    public async Task<ErrorOr<AuthenticationResponse>> LoginAsync(LoginRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
        {
            return AuthenticationErrors.InvalidCredentials;
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return AuthenticationErrors.InvalidCredentials;
        }

        var role = _userManager.GetRolesAsync(user).Result.First();
        var accessToken = _jwtTokenGenerator.GetAccessToken(user, role);

        return user.ToAuthResponse(accessToken, Roles.Visitor);
    }
}