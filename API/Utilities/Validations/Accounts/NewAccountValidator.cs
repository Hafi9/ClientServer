using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class NewAccountValidator : AbstractValidator<NewAccountDto>
{
    public NewAccountValidator()
    {
        RuleFor(e => e.Password)
            .NotEmpty();
        RuleFor(e => e.OTP)
            .NotEmpty();
        RuleFor(e => e.ExpiredTime)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(10));

    }
}