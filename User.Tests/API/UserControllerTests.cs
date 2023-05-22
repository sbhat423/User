using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using User.API.Controllers;
using User.Business.Interfaces;
using User.Domain.Models;

namespace User.Tests.API
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task RetrieveUsers_ValidUsernames_ReturnsOkResult()
        {
            // Arrange
            var usernames = new List<string> { "Jason Smith", "John Doe" };
            IEnumerable<UserDetailsResponseModel> userDetailsResponseModels = new List<UserDetailsResponseModel>
            {
                new UserDetailsResponseModel
                {
                    Id = 1,
                    Name = "Jason Smith",
                    Login = "JasonSmith",
                    Company = "Good Company",
                    Followers = 8,
                    PublicRepos = 43,
                    AverageFollowers = 0.18604651f
                },
                new UserDetailsResponseModel
                {
                    Id = 2,
                    Name = "John Doe",
                    Login = "JohnDoe",
                    Company = "Incredible Inc",
                    Followers = 8,
                    PublicRepos = 43,
                    AverageFollowers = 0.18604651f
                },
            };
            _userServiceMock.Setup(x => x.GetUserDetails(usernames))
                            .Returns(Task.FromResult(userDetailsResponseModels));

            // Act
            var result = await _controller.RetrieveUsers(usernames) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Same(userDetailsResponseModels, result.Value);
        }

        [Fact]
        public async Task RetrieveUsers_NullUsernames_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.RetrieveUsers(null) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The list of usernames is null or empty", result.Value);
        }

        [Fact]
        public async Task RetrieveUsers_EmptyUsernames_ReturnsBadRequest()
        {
            // Arrange
            var usernames = new List<string>();

            // Act
            var result = await _controller.RetrieveUsers(usernames) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The list of usernames is null or empty", result.Value);
        }

        [Fact]
        public async Task RetrieveUsers_ContainsNullOrEmptyUsernames_ReturnsBadRequest()
        {
            // Arrange
            var usernames = new List<string> { "user1", null, "" };

            // Act
            var result = await _controller.RetrieveUsers(usernames) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The usernames list item should not contain null or empty values", result.Value);
        }

        [Fact]
        public async Task RetrieveUsers_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            var usernames = new List<string> { "user1", "user2" };
            _userServiceMock.Setup(x => x.GetUserDetails(usernames)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.RetrieveUsers(usernames) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error: Test exception", result.Value);
        }
    }
}
