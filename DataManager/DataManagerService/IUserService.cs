using BankingData;

namespace DataManagerService
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        public User GetUserById(string code);
    }
}
