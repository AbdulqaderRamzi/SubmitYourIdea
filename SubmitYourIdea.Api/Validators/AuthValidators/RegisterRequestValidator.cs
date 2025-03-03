using FluentValidation;
using SubmitYourIdea.ApiModels;
using SubmitYourIdea.ApiModels.Auth;

namespace SubmitYourIdea.Api.Validators.AuthValidators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}