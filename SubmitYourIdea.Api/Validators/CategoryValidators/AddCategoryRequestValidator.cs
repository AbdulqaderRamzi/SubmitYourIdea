using FluentValidation;
using FluentValidation.Results;
using SubmitYourIdea.ApiModels.Category;

namespace SubmitYourIdea.Api.Validators.CategoryValidators;

public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
{
    public AddCategoryRequestValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name cannot be empty");
    }
}