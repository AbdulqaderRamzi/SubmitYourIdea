using FluentValidation;
using SubmitYourIdea.ApiModels.Idea;

namespace SubmitYourIdea.Api.Validators.IdeaValidators;

public class AddIdeaRequestValidator : AbstractValidator<AddIdeaRequest>
{
    public AddIdeaRequestValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage("Title cannot be empty");
        RuleFor(r => r.Description).NotEmpty().WithMessage("Description cannot be empty");
        RuleFor(r => r.CategoryId).NotEmpty().WithMessage("Category cannot be empty");
    }
}