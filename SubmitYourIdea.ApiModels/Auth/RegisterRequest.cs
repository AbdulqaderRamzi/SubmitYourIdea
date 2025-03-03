namespace SubmitYourIdea.ApiModels.Auth;

public record RegisterRequest(
    string FirstName, 
    string LastName,
    string Email, 
    string Password, 
    string ConfirmPassword);
