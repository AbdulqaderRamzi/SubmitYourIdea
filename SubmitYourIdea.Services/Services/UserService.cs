using System.Net;
using Microsoft.AspNetCore.Identity;
using SubmitYourIdea.ApiModels.Api;
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

    public async Task<ApiResponse<AuthenticationResponse>> Register(RegisterRequest request)
    {
        var user = request.ToAppUser();
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return AuthenticationErrors.InvalidOperation<AuthenticationResponse>();
        }
        
        await _userManager.AddToRoleAsync(user, Roles.Visitor);

        var accessToken = _jwtTokenGenerator.GetAccessToken(user, Roles.Visitor);

        return new ApiResponse<AuthenticationResponse>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = user.ToAuthResponse(accessToken, Roles.Visitor)
        };
    }

    public async Task<ApiResponse<AuthenticationResponse>> Login(LoginRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
        {
            return AuthenticationErrors.InvalidCredentials<AuthenticationResponse>();
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return AuthenticationErrors.InvalidCredentials<AuthenticationResponse>();
        }

        var role = _userManager.GetRolesAsync(user).Result.First();
        var accessToken = _jwtTokenGenerator.GetAccessToken(user, role);

        return new ApiResponse<AuthenticationResponse>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = user.ToAuthResponse(accessToken, role)
        };
    }
}