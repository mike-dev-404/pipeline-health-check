using BankingData;
using DataManagerService;
using DataManagerWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DataManagerWebAppTest
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserService> _serverMock;

        public UserControllerTest()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _serverMock = new Mock<IUserService>();
        }

        #region GetUser
        [Fact]
        public void GetUser_UserFound()
        {
            // Arrange
            var controller = CreateController();

            _serverMock
                .Setup(s => s.GetUserById(It.IsAny<string>()))
                .Returns(new User { Id = 1, Username = "user1", Birthday = new DateOnly(2025, 01, 01), Email = "user.1@github.com", Nationality = "Worldwide" });

            // Act
            var response = controller.GetUser("1");

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((OkObjectResult)response).StatusCode);

            var user = ((OkObjectResult)response).Value;
            Assert.IsType<User>(user);
            Assert.Equal("user1", ((User)user).Username);
        }

        [Fact]
        public void GetUser_UserNotFound()
        {
            // Arrange
            var controller = CreateController();

            User? userResult = null;
            _serverMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Returns(userResult);

            // Act
            var response = controller.GetUser("1");

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, ((NotFoundObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetUser_ThrowKeyNotFoundException()
        {
            // Arrange
            var controller = CreateController();

            _serverMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Throws(new KeyNotFoundException("User not found: 1"));

            // Act
            var response = controller.GetUser("1");

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, ((NotFoundObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetUser_ThrowGenericException()
        {
            // Arrange
            var controller = CreateController();

            _serverMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Throws(new Exception("User not found: 1"));

            // Act
            var response = controller.GetUser("1");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(500, ((ObjectResult)response).StatusCode);
        }
        #endregion

        #region GetAllUsers
        [Fact]
        public void GetAllUsers_UsersFound()
        {
            // Arrange
            var controller = CreateController();

            _serverMock
                .Setup(s => s.GetAllUsers())
                .Returns(
                [
                    new User { Id = 1, Username = "user1", Birthday = new DateOnly(2025, 01, 01), Email = "user.1@github.com", Nationality = "Poland" },
                    new User { Id = 2, Username = "user2", Birthday = new DateOnly(2025, 12, 12), Email = "user.2@github.com", Nationality = "Poland" },
                    new User { Id = 3, Username = "user3", Birthday = new DateOnly(2025, 10, 12), Email = "user.3@github.com", Nationality = "United States" },
                ]);

            // Act
            var response = controller.GetAllUsers();

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((OkObjectResult)response).StatusCode);

            var users = ((OkObjectResult)response).Value;
            Assert.IsAssignableFrom<IEnumerable<User>>(users);
            Assert.Equal(3, ((IEnumerable<User>)users).Count());
        }

        [Fact]
        public void GetAllUsers_ThrowGenericException()
        {
            // Arrange
            var controller = CreateController();

            User? userResult = null;
            _serverMock.Setup(s => s.GetAllUsers())
                .Throws(new Exception("User not found"));

            // Act
            var response = controller.GetAllUsers();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(500, ((ObjectResult)response).StatusCode);
        }
        #endregion

        #region GetUsersByNationality
        [Fact]
        public void GetUsersByNationality_UsersFound()
        {
            // Arrange
            var controller = CreateController();

            _serverMock
                .Setup(s => s.GetAllUsersByNationality(It.IsAny<string>()))
                .Returns(
                [
                    new User { Id = 1, Username = "user1", Birthday = new DateOnly(2025, 01, 01), Email = "user.1@github.com", Nationality = "Poland" },
                    new User { Id = 2, Username = "user2", Birthday = new DateOnly(2025, 12, 12), Email = "user.2@github.com", Nationality = "Poland" },
                ]);

            // Act
            var response = controller.GetUsersByNationality("Poland");

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((OkObjectResult)response).StatusCode);

            var users = ((OkObjectResult)response).Value;
            Assert.IsAssignableFrom<IEnumerable<User>>(users);
            Assert.Equal(2, ((IEnumerable<User>)users).Count());
        }

        [Fact]
        public void GetUsersByNationality_ThrowKeyNotFoundException()
        {
            // Arrange
            var controller = CreateController();

            _serverMock.Setup(s => s.GetAllUsersByNationality(It.IsAny<string>()))
                .Throws(new KeyNotFoundException("Users not found by nationality: Poland"));

            // Act
            var response = controller.GetUsersByNationality("Poland");

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, ((NotFoundObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetUsersByNationality_ThrowGenericException()
        {
            // Arrange
            var controller = CreateController();

            _serverMock.Setup(s => s.GetAllUsersByNationality(It.IsAny<string>()))
                .Throws(new Exception("Users not found by nationality: Poland"));

            // Act
            var response = controller.GetUsersByNationality("Poland");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(500, ((ObjectResult)response).StatusCode);
        }
        #endregion

        private UserController CreateController()
         => new UserController(_loggerMock.Object, _serverMock.Object);
    }
}
