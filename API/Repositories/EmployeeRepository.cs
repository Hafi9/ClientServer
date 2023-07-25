using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : ApiRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context) { }

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                           .SingleOrDefault(employee=>employee.Email.Contains(value) || employee.PhoneNumber.Contains(value)) is null;
        }
        
        public string? Getlastnik()
        {
            var data = _context.Set<Employee>().OrderByDescending(e => e.CreatedDate).FirstOrDefault().NIK;
            return data;
        }
        public Employee? GetByEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }

    }
}
