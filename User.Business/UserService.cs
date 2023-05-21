using User.Business.Helpers;
using User.Business.Interfaces;
using User.DataAccess.Interfaces;
using User.Domain.Models;

namespace User.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDetailsResponseModel>> GetUserDetails(IEnumerable<string> userNames)
        {
            var distinctUserNames = userNames.Distinct().ToList();
            List<UserDetailsResponseModel> result = new List<UserDetailsResponseModel>();

            List<Task<UserDetails>> tasks = distinctUserNames
                .Select(username => _userRepository.GetUserDetails(username))
                .ToList();
            
            UserDetails[] userDetailResults = await Task.WhenAll(tasks);

            return userDetailResults
                .Where(x => x != null)
                .Select(x => UserDetailsHelper.GetUserDetailsResponseModel(x))
                .OrderBy(x => x.Name).ToList();
        }
    }
}
