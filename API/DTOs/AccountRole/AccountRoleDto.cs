using API.Models;
using System;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(AccountRoleDto accountRoleDto)
        {
            return new AccountRole
            {
                Guid = accountRoleDto.Guid,
                AccountGuid = accountRoleDto.AccountGuid,
                RoleGuid = accountRoleDto.RoleGuid,
                // Assuming there are other properties in the AccountRole entity, add them here if needed.
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator AccountRoleDto(AccountRole accountRole)
        {
            return new AccountRoleDto
            {
                Guid = accountRole.Guid,
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid
                // Assuming there are other properties in the AccountRole entity, add them here if needed.
            };
        }
    }
}
