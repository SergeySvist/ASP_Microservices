using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.DTO;

namespace UsersMS.Client
{
    public interface IUserMsClient
    {
        public Task<UserDTO> GetUserByID(long id);
        public Task<UserDTO> CreateUser(AddUserDTO userToAdd);
    }
}
