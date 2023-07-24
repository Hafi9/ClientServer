using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities;

public class NewUniversityValidator : AbstractValidator<NewUniversityDto>
{
    public NewUniversityValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty();
        RuleFor(u => u.Code)
            .NotEmpty();
    }
}