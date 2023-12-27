using UsersMS.DTO;

namespace UserMS.CacheClient
{
    public interface IUserMSCacheClient
    {
        bool AddUserFromUserDTO(UserDTO userDTO);
        bool DeleteUserById(long id);
        UserDTO? GetUserById(long id);
    }
}