using System.ComponentModel;

namespace User.Domain.Models
{
    public class UserDetailsResponseModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Login { get; set; }
        public string? Company { get; set; }
        public int Followers { get; set; }
        public int PublicRepos { get; set; }
        [Description("Average followers per public repository")]
        public float AverageFollowers { get; set; }
    }
}
