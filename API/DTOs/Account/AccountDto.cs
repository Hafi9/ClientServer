using API.Models;
using System;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                IsDeleted = accountDto.IsDeleted,
                OTP = accountDto.OTP,
                IsUsed = false,
                ExpiredTime = accountDto.ExpiredTime,
                CreatedDate = accountDto.CreatedDate,
                ModifiedDate = accountDto.ModifiedDate
            };
        }

        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                OTP = account.OTP,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime,
                CreatedDate = account.CreatedDate,
                ModifiedDate = account.ModifiedDate
            };
        }
    }
}
