using User.Domain.Models;

namespace User.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailsResponseModel>> GetUserDetails(IEnumerable<string> userNames);
    }
}
