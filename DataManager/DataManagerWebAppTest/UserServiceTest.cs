using BankingData;
using DataManagerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagerWebAppTest
{
    public class UserServiceTest
    {
        #region GetAllUsers
        [Fact]
        public void GetAllUsers_ReturnUsers()
        {
            // Arrange
            var service = GetService();

            // Act
            var user = service.GetAllUsers();

            // Assert
            Assert.NotNull(user);
            Assert.True(user.Count() > 0);
        }
        #endregion

        #region GetUserById
        [Fact]
        public void GetUserById_ReturnMatchedUser()
        {
            // Arrange
            var service = GetService();

            // Act
            var user = service.GetUserById("gezeleb2");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("gezeleb2", user.Username);
        }

        [Fact]
        public void GetUserById_ThrowsKeyNotFoundException()
        {
            // Arrange
            var service = GetService();

            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.GetUserById("nonexistentuser"));
        }
        #endregion

        #region GetAllUsersByNationality
        [Fact]
        public void GetAllUsersByNationality_ReturnMatchedUsers()
        {
            // Arrange
            var service = GetService();

            // Act
            var users = service.GetAllUsersByNationality("Iran");

            // Assert
            Assert.NotNull(users);
            Assert.True(users.Count() > 0);
        }

        [Fact]
        public void GetAllUsersByNationality_ThrowsKeyNotFoundException()
        {
            // Arrange
            var service = GetService();

            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.GetAllUsersByNationality("AAAAAAAAAAAAAa"));
        }
        #endregion

        private IUserService GetService()
            => new UserService();
    }
}
