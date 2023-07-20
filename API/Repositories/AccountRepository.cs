using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRepository : ApiRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingDbContext context) : base(context) { }


    }
}
