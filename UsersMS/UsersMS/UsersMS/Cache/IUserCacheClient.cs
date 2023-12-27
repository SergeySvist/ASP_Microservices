using UsersMS.DTO;

namespace UsersMS.Cache
{
    public interface IUserCacheClient
    {
        bool AddUserFromUserDTO(UserDTO userDTO);
        bool DeleteUserById(long id);
        UserDTO? GetUserById(long id);
    }
}