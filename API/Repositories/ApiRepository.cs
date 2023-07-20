using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class ApiRepository<APR> : IApiRepository<APR> where APR : class
    {
        protected readonly BookingDbContext _context;

        public ApiRepository(BookingDbContext context)
        {
            _context = context;
        }

        public ICollection<APR> GetAll()
        {
            return _context.Set<APR>()
                           .ToList();
        }

        public APR? GetByGuid(Guid guid)
        {
            var apr = _context.Set<APR>()
                              .Find(guid);
            _context.ChangeTracker.Clear();
            return apr;
        }

        public APR? Create(APR apr)
        {
            try
            {
                _context.Set<APR>()
                        .Add(apr);
                _context.SaveChanges();
                return apr;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(APR apr)
        {
            try
            {
                _context.Set<APR>()
                        .Update(apr);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(APR apr)
        {
            try
            {
                _context.Set<APR>()
                        .Remove(apr);
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

