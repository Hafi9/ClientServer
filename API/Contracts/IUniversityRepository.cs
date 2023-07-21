using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IApiRepository<University>
{
    IEnumerable<University> GetByName(string name);
}
