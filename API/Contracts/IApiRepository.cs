using API.Models;

namespace API.Contracts
{
    public interface IApiRepository<APR>
    {
        ICollection<APR> GetAll();
        APR? GetByGuid(Guid guid);
        APR? Create(APR apr);
        bool Update(APR apr);
        bool Delete(APR apr);
    }
}
