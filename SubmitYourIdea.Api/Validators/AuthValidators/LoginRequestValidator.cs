using FluentValidation;
using SubmitYourIdea.ApiModels;
using SubmitYourIdea.ApiModels.Auth;

namespace SubmitYourIdea.Api.Validators.AuthValidators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}