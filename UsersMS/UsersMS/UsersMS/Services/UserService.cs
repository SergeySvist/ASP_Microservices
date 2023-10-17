using Mapster;
using UsersMS.Domain.DataLayer;
using UsersMS.Domain.Models;
using UsersMS.DTO;

namespace UsersMS.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserDataLayer _userDataLayer;

        public UserService (IPasswordService passwordService, IUserDataLayer userDataLayer)
        {
            _passwordService = passwordService;
            _userDataLayer = userDataLayer;
        }

        public async Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd)
        {
            User newuser = userToAdd.Adapt<User>();
            newuser.PasswordHash = _passwordService.CreatePasswordHash(userToAdd.Password);

            User addedUser = await _userDataLayer.AddUser(newuser);

            return addedUser.Adapt<UserDTO>();

        }

        public async Task<UserDTO> GetUserById(long id)
        {
            User userById = await _userDataLayer.GetUserById(id);

            UserDTO userDTO = userById.Adapt<UserDTO>();

            return userDTO;
        }
    }
}
