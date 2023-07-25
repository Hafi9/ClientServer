using API.DTOs.Accounts;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Accounts
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(login => login.Password).NotEmpty();
        }
    }
}