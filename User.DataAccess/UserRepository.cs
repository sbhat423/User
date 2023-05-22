using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using User.DataAccess.Interfaces;
using User.Domain.Models;

namespace User.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IHttpClientFactory httpClientFactory, ILogger<UserRepository> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserDetails?> GetUserDetails(string username)
        {
            UserDetails? userDetail = null;

            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("Username is null or empty");
                throw new ArgumentException("Invalid usernames");
            }

            try
            {
                var client = _httpClientFactory.CreateClient(Constants.Source.GitHub);
                var response = client.GetAsync($"users/{username}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    userDetail = JsonConvert.DeserializeObject<UserDetails>(responseBody);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogWarning("UserName not found: {userName}", username);
                }

                return userDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get user details", ex.Message);
                throw;
            }
        }
    }
}
