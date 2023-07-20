using Microsoft.EntityFrameworkCore;
using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly BookingDbContext _context;

        public UniversityRepository(BookingDbContext context)
        {
            _context = context;
        }
        public University? Create(University university)
        {
            try
            {
                _context.Set<University>()
                    .Add(university);
                _context.SaveChanges();
                return university;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(University university)
        {
            try
            {
                _context.Set<University>()
                        .Remove(university);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>()
                           .ToList();
        }

        public University? GetByGuid(Guid guid)
        {
            return _context.Set<University>()
                           .Find(guid);
        }

        public bool Update(University university)
        {
            try
            {
                _context.Entry(university)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
