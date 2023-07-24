using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class NewEmployeeValidator : AbstractValidator<NewEmployeeDto>
    {
        public NewEmployeeValidator()
        {
            RuleFor(employee => employee.NIK)
                .NotEmpty();

            RuleFor(employee => employee.FirstName)
                .NotEmpty();

            RuleFor(employee => employee.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

            RuleFor(employee => employee.Gender) 
                .NotEmpty()
                .IsInEnum();

            RuleFor(employee => employee.HiringDate)
                .NotEmpty();

            RuleFor(employee => employee.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(employee=>employee.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches("");
        }
    }
}
