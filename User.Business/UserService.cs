using Microsoft.Extensions.Logging;
using User.Business.Helpers;
using User.Business.Interfaces;
using User.DataAccess.Interfaces;
using User.Domain.Models;

namespace User.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<UserDetailsResponseModel>> GetUserDetails(IEnumerable<string> usernames)
        {
            List<UserDetailsResponseModel> result = new List<UserDetailsResponseModel>();
            
            if (usernames == null || !usernames.Any())
            {
                _logger.LogWarning("The list of usernames is null or empty");
                throw new ArgumentException("Invalid list of usernames");
            }
            
            _logger.LogInformation("Number of items in the usernames {count}", usernames.Count());

            var distinctUsernames = usernames.Distinct().ToList();
            _logger.LogInformation("Number of distinct items in the usernames {count}", distinctUsernames.Count());

            List<Task<UserDetails?>> tasks = distinctUsernames
                .Select(username => _userRepository.GetUserDetails(username))
                .ToList();
            
            UserDetails?[] userDetailResults = await Task.WhenAll(tasks);

            return userDetailResults
                .Where(x => x != null)
                .Select(x => UserDetailsHelper.GetUserDetailsResponseModel(x!))
                .OrderBy(x => x.Name).ToList();
        }
    }
}
