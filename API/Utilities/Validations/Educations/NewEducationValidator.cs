using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations;

public class NewEducationValidator : AbstractValidator<NewEducationDto>
{
    public NewEducationValidator()
    {
        RuleFor(ed => ed.Major)
            .NotEmpty();
        RuleFor(ed => ed.Degree)
            .NotEmpty();
        RuleFor(ed => ed.GPA)
            .NotEmpty();
        RuleFor(ed => ed.UniversityGuid)
            .NotEmpty();
    }
}