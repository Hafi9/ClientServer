using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IApiRepository<Employee>
    {
        bool IsNotExist(string value);
        public string? Getlastnik();
        Employee? GetByEmail(string email);
        public Employee? CheckEmail(string email);
        public Guid GetLastEmployeeGuid();
    }
}
