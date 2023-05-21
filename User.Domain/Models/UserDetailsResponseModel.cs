namespace User.Domain.Models
{
    public class UserDetailsResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Company { get; set; }
        public int Followers { get; set; }
        public int PublicRepos { get; set; }
        public float AverageFollowersPerPublicRepository { get; set; }
    }
}
