using API.Models;

namespace API.Contracts
{
    public interface IAccountRepository : IApiRepository<Account>
    {
        public bool IsNotExist(string value);
    }
}
