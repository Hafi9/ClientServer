using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class NewAccountRoleValidator : AbstractValidator<NewAccountRoleDto>
{
    public NewAccountRoleValidator()
    {
        RuleFor(ar => ar.AccountGuid)
            .NotEmpty();
        RuleFor(ar => ar.RoleGuid)
            .NotEmpty();
    }
}