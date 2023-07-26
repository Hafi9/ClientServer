using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IApiRepository<University>
{
    IEnumerable<University> GetByName(string name);
    University? GetByCode(string code);
    public University? IsExist(string value);
}
