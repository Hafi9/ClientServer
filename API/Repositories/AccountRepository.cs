using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRepository : ApiRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingDbContext context) : base(context) { }
        public Employee? GetByEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                .SingleOrDefault(e => e.Email.Contains(value) || e.PhoneNumber.Contains(value)) is null;
        }


    }
}
