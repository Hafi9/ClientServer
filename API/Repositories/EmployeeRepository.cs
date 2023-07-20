using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : ApiRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context) { }


    }
}
