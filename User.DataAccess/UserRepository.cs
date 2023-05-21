using Newtonsoft.Json;
using System.Net;
using User.DataAccess.Interfaces;
using User.Domain.Models;

namespace User.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserDetails> GetUserDetails(string userName)
        {
            var client = _httpClientFactory.CreateClient("GitHub");
            UserDetails userDetail = null;
            var response = client.GetAsync($"users/{userName}").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                userDetail = JsonConvert.DeserializeObject<UserDetails>(responseBody);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"UserName not found: {userName}");

                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            return userDetail;
        }
    }
}
