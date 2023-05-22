using Newtonsoft.Json;

namespace User.Domain.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Login { get; set; }
        public string? Company { get; set; }
        public int Followers { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }
    }
}
