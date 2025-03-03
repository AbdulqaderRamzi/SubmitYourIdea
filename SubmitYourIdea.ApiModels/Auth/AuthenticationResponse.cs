namespace SubmitYourIdea.ApiModels.Auth;

public record AuthenticationResponse(
    string UserId,
    string FullName,
    string Email, 
    string Role,
    string AccessToken);