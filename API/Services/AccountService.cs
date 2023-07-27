using System;
using System.Collections.Generic;
using System.Linq;
using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly BookingDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository,IUniversityRepository universityRepository, IEducationRepository educationRepository, BookingDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _dbContext = dbContext;
        }

        public IEnumerable<AccountDto> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return Enumerable.Empty<AccountDto>(); // Account is null or not found;
            }

            var accountDtos = new List<AccountDto>();
            foreach (var account in accounts)
            {
                accountDtos.Add((AccountDto)account);
            }

            return accountDtos; // Account is found;
        }
        public int Register(RegisterDto registerDto)
        {
            if (!_employeeRepository.IsNotExist(registerDto.Email) || !_employeeRepository.IsNotExist(registerDto.PhoneNumber))
            {
                return 0;
            }

            var newNik = GenerateHandler.Nik(_employeeRepository.Getlastnik());
            var employeeGuid = Guid.NewGuid();


            var employee = new Employee
            {
                Guid = employeeGuid,
                NIK = newNik,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                HiringDate = registerDto.HiringDate,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            _dbContext.Employees.Add(employee);


            var education = new Education
            {
                Guid = employeeGuid,
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                GPA = (float)registerDto.Gpa
            };
            _dbContext.Educations.Add(education);


            var existingUniversity = _universityRepository.GetByCode(registerDto.UniversityCode);
            if (existingUniversity is null)
            {

                var university = new University
                {
                    Code = registerDto.UniversityCode,
                    Name = registerDto.UniversityName
                };
                _dbContext.Universities.Add(university);


                education.UniversityGuid = university.Guid;
            }
            else
            {

                education.UniversityGuid = existingUniversity.Guid;
            }


            var account = new Account
            {
                Guid = employeeGuid,
                OTP = registerDto.Otp,
                Password = registerDto.Password
            };
            _dbContext.Accounts.Add(account);

            try
            {
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int Login(LoginDto loginDto)
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);
            if (getEmployee == null)
            {
                return 0;
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);
            if (getAccount.Password == loginDto.Password)
            {
                return 1;
            }

            return 0;
        }
        public int ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);
            if (employee is null)
            {
                return 0;
            }

            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
            {
                return 0;
            }

            var otp = new Random().Next(111111, 999999);
            var isUpdate = _accountRepository.Update(new Account
            {
                Guid = account.Guid,
                Password = account.Password,
                ExpiredTime = DateTime.Now.AddMinutes(5),
                OTP = otp,
                IsUsed = false,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            });

            if (!isUpdate)
            {
                return -1;
            }

            forgotPasswordDto.Email = $"{otp}";
            return 1;
        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var isExist = _employeeRepository.CheckEmail(changePasswordDto.Email);
            if (isExist is null)
            {
                return -1;
            }

            var getAccount = _accountRepository.GetByGuid(isExist.Guid);
            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                OTP = getAccount.OTP,
                ExpiredTime = getAccount.ExpiredTime,
                Password = changePasswordDto.NewPassword
            };

            if (getAccount.OTP != changePasswordDto.Otp)
            {
                return 0;
            }

            if (getAccount.IsUsed == true)
            {
                return 1;
            }

            if (getAccount.ExpiredTime < DateTime.Now)
            {
                return 2;
            }

            var isUpdate = _accountRepository.Update(account);
            if (!isUpdate)
            {
                return 0;
            }
            return 3;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var account = _accountRepository.Create(newAccountDto);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public int Update(AccountDto accountDto)
        {
            var account = _accountRepository.GetByGuid(accountDto.Guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            Account toUpdate = accountDto;
            // You may update other properties here if needed
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 // Account is updated;
                : 0; // Account failed to update;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            var result = _accountRepository.Delete(account);

            return result ? 1 // Account is deleted;
                : 0; // Account failed to delete;
        }
    }
}
