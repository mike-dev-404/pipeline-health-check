using BankingData;
using DataManagerService.Utilities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataManagerService
{
    public class UserService : IUserService
    {
        public IEnumerable<User> GetAllUsers()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Users.json");
            if (!File.Exists(filePath))
                return Enumerable.Empty<User>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new DateOnlyJsonConverter() }
            };

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<User>>(json, options) ?? new List<User>();
        }

        public IEnumerable<User> GetAllUsersByNationality(string nationality)
            => GetAllUsers().Where(user => Regex.Match(user.Nationality, nationality, RegexOptions.IgnoreCase).Success) ?? throw new KeyNotFoundException($"Users not found by nationality: {nationality}");

        public User GetUserById(string code)
            => GetAllUsers().FirstOrDefault(user => Regex.Match(user.Username, code, RegexOptions.IgnoreCase).Success) ?? throw new KeyNotFoundException($"User not found: {code}");
    }
}
