﻿using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        public RegisterValidator(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;

            RuleFor(e => e.FirstName)
                .NotEmpty();

            RuleFor(e => e.LastName)
                .NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

            RuleFor(e => e.Gender)
                .NotNull()
                //diambil dari utilities/enum/genderlevel.cs
                .IsInEnum();

            RuleFor(e => e.HiringDate)
                .NotEmpty();

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Email is not valid");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +");

            RuleFor(e => e.Major)
                .NotEmpty();

            RuleFor(e => e.Degree)
                .NotEmpty();

            RuleFor(e => e.GPA)
                .NotEmpty();

            RuleFor(u => u.UniversityCode)
                .NotEmpty()
                .Must(IsDuplicationValue).WithMessage("Email already exist");

            RuleFor(u => u.UniversityName)
                .NotEmpty();

            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[0-9])(?=.*[A-Z]).{8,}$").WithMessage("Password invalid! Passwords must have at least 1 upper case and 1 number");

            RuleFor(a => a.ConfirmPassword)
                .NotEmpty()
                .Equal(a => a.Password).WithMessage("Passwords do not match");
        }

        private bool IsDuplicationValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}