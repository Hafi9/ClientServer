using API.Models;
using System;

namespace API.DTOs.Roles
{
    public class NewRoleDto
    {
        public string Name { get; set; }

        public static implicit operator Role(NewRoleDto newRoleDto)
        {
            return new Role
            {
                Guid = Guid.NewGuid(), // Assuming Guid is the primary key of the "Role" entity and generating a new Guid for the new Role entry
                Name = newRoleDto.Name
            };
        }

        public static explicit operator NewRoleDto(Role role)
        {
            return new NewRoleDto
            {
                Name = role.Name
            };
        }
    }
}
