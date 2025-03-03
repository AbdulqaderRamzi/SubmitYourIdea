using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GetAccessToken(AppUser user, string role);
}