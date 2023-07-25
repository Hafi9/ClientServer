using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IApiRepository<Employee>
    {
        bool IsNotExist(string value);
        string GetLastNik();
    }
}
