using API.Contracts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class NewEmployeeValidator : AbstractValidator<NewEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public NewEmployeeValidator(IEmployeeRepository  employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(employee => employee.FirstName)
                .NotEmpty();

            RuleFor(employee => employee.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

            RuleFor(employee => employee.Gender) 
                .NotNull()
                .IsInEnum();

            RuleFor(employee => employee.HiringDate)
                .NotEmpty();

            RuleFor(employee => employee.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(IsDuplicateValue);

            RuleFor(employee=>employee.PhoneNumber)
                .NotEmpty()
                .MaximumLength(13)
                .MinimumLength(11)
                .Matches(@"^\+[0-9]")
                .Must(IsDuplicateValue);
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
