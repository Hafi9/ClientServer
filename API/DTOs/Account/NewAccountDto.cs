using API.Models;
using System;

namespace API.DTOs.Accounts
{
    public class NewAccountDto
    {
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Account(NewAccountDto newAccountDto)
        {
            return new Account
            {
                Guid = Guid.NewGuid(), // Assuming Guid is the primary key of the "Account" entity and generating a new Guid for the new Account entry
                Password = newAccountDto.Password,
                IsDeleted = newAccountDto.IsDeleted,
                OTP = newAccountDto.OTP,
                IsUsed = newAccountDto.IsUsed,
                ExpiredTime = newAccountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewAccountDto(Account account)
        {
            return new NewAccountDto
            {
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                OTP = account.OTP,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime
                // Assuming there are other properties in the Account entity, add them here if needed.
            };
        }
    }
}
