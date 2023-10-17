using UsersMS.Domain.Models;

namespace UsersMS.Domain.DataLayer
{
    public interface IUserDataLayer
    {
        Task<User> AddUser(User newuser);
        Task<User> GetUserById(long id);

    }
}
