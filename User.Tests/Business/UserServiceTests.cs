using Microsoft.Extensions.Logging;
using Moq;
using User.Business;
using User.DataAccess.Interfaces;
using User.Domain.Models;

namespace User.Tests.Business
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUserDetails_ValidUsernames_ReturnsUserDetailsResponseModels()
        {
            // Arrange
            var usernames = new List<string> { "JohnDoe", "JaneSmith" };
            
            var userDetails1 = new UserDetails
            {
                Id = 1,
                Name = "JohnDoe",
                Login = "johndoe",
                Company = "ABC Inc",
                Followers = 100,
                PublicRepos = 10
            };
            var userDetails2 = new UserDetails
            {
                Id = 2,
                Name = "JaneSmith",
                Login = "janesmith",
                Company = "XYZ Corp",
                Followers = 50,
                PublicRepos = 5
            };

            _userRepositoryMock.Setup(x => x.GetUserDetails("JohnDoe")).ReturnsAsync(userDetails1);
            _userRepositoryMock.Setup(x => x.GetUserDetails("JaneSmith")).ReturnsAsync(userDetails2);

            // Act
            var result = await _userService.GetUserDetails(usernames);

            // Assert
            Assert.NotNull(result);
            var responseModels = result.ToList();
            Assert.Equal(2, responseModels.Count);

            Assert.Equal(userDetails2.Id, responseModels[0].Id);
            Assert.Equal(userDetails2.Name, responseModels[0].Name);
            Assert.Equal(userDetails2.Login, responseModels[0].Login);
            Assert.Equal(userDetails2.Company, responseModels[0].Company);
            Assert.Equal(userDetails2.Followers, responseModels[0].Followers);
            Assert.Equal(userDetails2.PublicRepos, responseModels[0].PublicRepos);

            Assert.Equal(userDetails1.Id, responseModels[1].Id);
            Assert.Equal(userDetails1.Name, responseModels[1].Name);
            Assert.Equal(userDetails1.Login, responseModels[1].Login);
            Assert.Equal(userDetails1.Company, responseModels[1].Company);
            Assert.Equal(userDetails1.Followers, responseModels[1].Followers);
            Assert.Equal(userDetails1.PublicRepos, responseModels[1].PublicRepos);
        }

        [Fact]
        public async Task GetUserDetails_NullUsernames_ThrowsArgumentException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserDetails(null));
        }

        [Fact]
        public async Task GetUserDetails_EmptyUsernames_ThrowsArgumentException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserDetails(new List<string>()));
        }

        [Fact]
        public async Task GetUserDetails_DuplicateUsernames_ReturnsDistinctUserDetailsResponseModels()
        {
            // Arrange
            var usernames = new List<string> { "JohnDoe", "JaneSmith", "JohnDoe" };

            var userDetails1 = new UserDetails
            {
                Id = 1,
                Name = "JohnDoe",
                Login = "johndoe",
                Company = "ABC Inc",
                Followers = 100,
                PublicRepos = 10
            };
            var userDetails2 = new UserDetails
            {
                Id = 2,
                Name = "JaneSmith",
                Login = "janesmith",
                Company = "XYZ Corp",
                Followers = 50,
                PublicRepos = 5
            };

            _userRepositoryMock.Setup(x => x.GetUserDetails("JohnDoe")).ReturnsAsync(userDetails1);
            _userRepositoryMock.Setup(x => x.GetUserDetails("JaneSmith")).ReturnsAsync(userDetails2);

            // Act
            var result = await _userService.GetUserDetails(usernames);

            // Assert
            Assert.NotNull(result);
            var responseModels = result.ToList();
            Assert.Equal(2, responseModels.Count);

            Assert.Equal(userDetails2.Id, responseModels[0].Id);
            Assert.Equal(userDetails2.Name, responseModels[0].Name);
            Assert.Equal(userDetails2.Login, responseModels[0].Login);
            Assert.Equal(userDetails2.Company, responseModels[0].Company);
            Assert.Equal(userDetails2.Followers, responseModels[0].Followers);
            Assert.Equal(userDetails2.PublicRepos, responseModels[0].PublicRepos);

            Assert.Equal(userDetails1.Id, responseModels[1].Id);
            Assert.Equal(userDetails1.Name, responseModels[1].Name);
            Assert.Equal(userDetails1.Login, responseModels[1].Login);
            Assert.Equal(userDetails1.Company, responseModels[1].Company);
            Assert.Equal(userDetails1.Followers, responseModels[1].Followers);
            Assert.Equal(userDetails1.PublicRepos, responseModels[1].PublicRepos);
        }
    }
}

