using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UniversityRepository : ApiRepository<University>, IUniversityRepository
{
    public UniversityRepository(BookingDbContext context) : base(context) { }
    public University? IsExist(string code)
    {
        return _context.Set<University>().SingleOrDefault(u => u.Code.Contains(code));
    }

    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>()
                       .Where(university => university.Name.Contains(name))
                       .ToList();
    }
    public University? GetByCode(string code)
    {
        return _context.Set<University>().SingleOrDefault(u => u.Code == code);
    }
}
