using User.Domain.Models;

namespace User.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDetails?> GetUserDetails(string userNames);
    }
}
