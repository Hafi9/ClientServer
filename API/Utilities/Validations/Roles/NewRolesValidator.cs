using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class NewRoleValidator : AbstractValidator<NewRoleDto>
{
    public NewRoleValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}