using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : ApiRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookingDbContext context) : base(context) { }


    }
}
