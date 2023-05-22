using User.Business.Helpers;
using User.Domain.Models;

namespace User.Tests.Business.Helpers
{
    public class UserDetailsHelperTests
    {
        [Fact]
        public void GetUserDetailsResponseModel_ValidUserDetails_ReturnsExpectedResponseModel()
        {
            // Arrange
            UserDetails userDetails = new UserDetails
            {
                Id = 1,
                Name = "John Doe",
                Login = "johndoe",
                Company = "ABC Inc",
                Followers = 100,
                PublicRepos = 10
            };

            UserDetailsResponseModel expectedResponseModel = new UserDetailsResponseModel
            {
                Id = userDetails.Id,
                Name = userDetails.Name,
                Login = userDetails.Login,
                Company = userDetails.Company,
                Followers = userDetails.Followers,
                PublicRepos = userDetails.PublicRepos,
                AverageFollowers = 10.0f
            };

            // Act
            var result = UserDetailsHelper.GetUserDetailsResponseModel(userDetails);

            // Assert
            Assert.Equal(expectedResponseModel.Id, result.Id);
            Assert.Equal(expectedResponseModel.Name, result.Name);
            Assert.Equal(expectedResponseModel.Login, result.Login);
            Assert.Equal(expectedResponseModel.Company, result.Company);
            Assert.Equal(expectedResponseModel.Followers, result.Followers);
            Assert.Equal(expectedResponseModel.PublicRepos, result.PublicRepos);
            Assert.Equal(expectedResponseModel.AverageFollowers, result.AverageFollowers);
        }
    }
}
