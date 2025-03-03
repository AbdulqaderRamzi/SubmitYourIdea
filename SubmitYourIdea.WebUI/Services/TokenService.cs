using MovieMate.Web.Services.IServices;

namespace SubmitYourIdea.WebUI.Services;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string AccessTokenKey = "access_token";

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetToken(string accessToken)
    {
        var options = GetCookieOptions();
        _httpContextAccessor.HttpContext?
            .Response.Cookies.Append(AccessTokenKey, accessToken, options);
    }
    
    public string? GetAccessToken()
    {
        return _httpContextAccessor.HttpContext?
            .Request.Cookies[AccessTokenKey];
    }
    

    public void ClearToken()
    {
        _httpContextAccessor.HttpContext?
            .Response.Cookies.Delete(AccessTokenKey);
    }

    private CookieOptions GetCookieOptions()
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };
    }
}