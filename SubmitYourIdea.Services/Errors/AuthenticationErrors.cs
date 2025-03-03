using ErrorOr;

namespace SubmitYourIdea.Services.Errors;

public static class AuthenticationErrors {
    
    public static Error RegistrationFailed(string? description)
    {
        return Error.Failure(
            code: "User.RegistrationFailed",
            description: description ?? "Registration failed."
        );
    }
    
    public static Error InvalidCredentials => Error.Validation(
        code: "User.InvalidCredentials", 
        description: "Invalid credentials"
    );
        
    public static Error ExpiredRefreshToken => Error.Validation(
        code: "User.ExpiredRefreshToken", 
        description: "The refresh token has expired"
    );
    
    public static Error AccessDenied => Error.Validation(
        code: "User.AccessDenied", 
        description: "Access denied"
    );
}
