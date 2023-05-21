using Newtonsoft.Json;

namespace User.Domain.Models
{
    public class UserDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Company { get; set; }
        public int Followers { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }
    }
}
