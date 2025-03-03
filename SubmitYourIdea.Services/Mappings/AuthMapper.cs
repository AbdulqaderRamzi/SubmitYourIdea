using Microsoft.AspNetCore.Identity;
using SubmitYourIdea.ApiModels;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.DataAccess.Entities;
using SubmitYourIdea.Services.Interfaces;

namespace SubmitYourIdea.Services.Mappings;

public static class AuthMapper
{

    public static AuthenticationResponse ToAuthResponse(this AppUser user, string accessToken, string role)
    {
        return new AuthenticationResponse
        (
            UserId: user.Id,
            FullName: $"{user.FirstName} {user.LastName}",
            Email: user.Email!,
            Role: role,
            AccessToken: accessToken
        );
    }

    public static AppUser ToAppUser(this RegisterRequest request)
    {
        return new AppUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
        };
    }
}