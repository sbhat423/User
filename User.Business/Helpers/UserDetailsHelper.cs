using User.Domain.Models;

namespace User.Business.Helpers
{
    public static class UserDetailsHelper
    {
        public static UserDetailsResponseModel GetUserDetailsResponseModel(UserDetails userDetails)
        {
            return new UserDetailsResponseModel
            {
                Id = userDetails.Id,
                Name = userDetails.Name,
                Login = userDetails.Login,
                Company = userDetails.Company,
                Followers = userDetails.Followers,
                PublicRepos = userDetails.PublicRepos,
                AverageFollowers = (float)userDetails.Followers / userDetails.PublicRepos,
            };
        }
    }
}
