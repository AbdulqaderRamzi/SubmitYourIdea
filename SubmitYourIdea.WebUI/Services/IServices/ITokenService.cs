namespace MovieMate.Web.Services.IServices;

public interface ITokenService
{
    string? GetAccessToken();
    void SetToken(string accessToken);
    void ClearToken();
}