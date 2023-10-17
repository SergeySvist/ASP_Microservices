using UsersMS.DTO;

namespace UsersMS.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd);
        Task<UserDTO> GetUserById(long id);
    }
}
