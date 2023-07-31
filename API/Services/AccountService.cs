using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;
        private readonly BookingDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, IEmailHandler emailHandler, BookingDbContext dbContext, ITokenHandler tokenHandler)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _emailHandler = emailHandler;
            _dbContext = dbContext;
            _tokenHandler = tokenHandler;
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
        public RegisterDto? Register(RegisterDto registerDto)
        {
            if (!_employeeRepository.IsNotExist(registerDto.Email) ||
                !_employeeRepository.IsNotExist(registerDto.PhoneNumber))
            {
                return null;
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var university = _universityRepository.GetByCode(registerDto.UniversityCode);
                if (university is null)
                {
                    var createUniversity = _universityRepository.Create(new University
                    {
                        Code = registerDto.UniversityCode,
                        Name = registerDto.UniversityName
                    });

                    university = createUniversity;
                }

                var newNik =
                    GenerateHandler.Nik(_employeeRepository
                                   .Getlastnik()); 
                var employeeGuid = Guid.NewGuid();

                var employee = _employeeRepository.Create(new Employee
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
                });


                var education = _educationRepository.Create(new Education
                {
                    Guid = employeeGuid,
                    Major = registerDto.Major,
                    Degree = registerDto.Degree,
                    GPA = registerDto.GPA,
                    UniversityGuid = university.Guid
                });

                var account = _accountRepository.Create(new Account
                {
                    Guid = employeeGuid,
                    OTP = 1,
                    IsUsed = true,
                    Password = HashingHandler.GenerateHash(registerDto.Password)
                });
                var data = new RegisterDto
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    BirthDate = employee.BirthDate,
                    PhoneNumber = employee.PhoneNumber,
                    Gender = employee.Gender,
                    HiringDate = employee.HiringDate,
                    Degree = education.Degree,
                    Major = education.Major,
                    GPA = education.GPA,
                    UniversityCode = university.Code,
                    UniversityName = university.Name
                };
                transaction.Commit();
                return data;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
        }
        public string Login(LoginDto loginDto)
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);
            if (getEmployee is null)
            {
                return "0";
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);
            var handlerPassword = HashingHandler.ValidateHash(loginDto.Password, getAccount.Password);
            if (!handlerPassword)
            {
                return "0";
            }

            var claims = new List<Claim>
        {
            new Claim("Guid", getEmployee.Guid.ToString()),
            new Claim("FullName", $"{getEmployee.FirstName} {getEmployee.LastName}"),
            new Claim("Email", getEmployee.Email)
        };

            var generatedToken = _tokenHandler.GenerateToken(claims);
            if (generatedToken is null)
            {
                return "-1";
            }
            return generatedToken;
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
            _emailHandler.SendEmail(forgotPasswordDto.Email,
                "Booking - Forgot Password", $"Your Otp is {otp}");
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
                Password = HashingHandler.GenerateHash(getAccount.Password)
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
