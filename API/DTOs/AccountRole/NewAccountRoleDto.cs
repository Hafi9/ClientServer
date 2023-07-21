using API.Models;
using System;

namespace API.DTOs.AccountRoles
{
    public class NewAccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(NewAccountRoleDto newAccountRoleDto)
        {
            return new AccountRole
            {
                Guid = Guid.NewGuid(), // Assuming Guid is the primary key of the "AccountRole" entity and generating a new Guid for the new AccountRole entry
                AccountGuid = newAccountRoleDto.AccountGuid,
                RoleGuid = newAccountRoleDto.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewAccountRoleDto(AccountRole accountRole)
        {
            return new NewAccountRoleDto
            {
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid
            };
        }
    }
}
