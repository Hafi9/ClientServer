using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRepository : ApiRepository<Account>, IAccountRepository
    {
        public Employee? GetByEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }
        public AccountRepository(BookingDbContext context) : base(context) { }


    }
}
