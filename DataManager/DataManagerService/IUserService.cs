using BankingData;

namespace DataManagerService
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(string code);

        IEnumerable<User> GetAllUsersByNationality(string nationality);
    }
}
