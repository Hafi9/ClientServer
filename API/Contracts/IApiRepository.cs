using API.Models;

namespace API.Contracts
{
    public interface IApiRepository<APR>
    {
        IEnumerable<APR> GetAll();
        APR? GetByGuid(Guid guid);
        APR? Create(APR apr);
        bool Update(APR apr);
        bool Delete(APR apr);
    }
}
