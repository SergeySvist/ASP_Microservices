using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersMS.DTO;
using UsersMS.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: api/<UsersController>
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(long id)
        {
            return await _userService.GetUserById(id);
        }


        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserDTO> Post([FromBody] AddUserDTO userToAdd)
        {
            return await _userService.CreateUserFromDTO(userToAdd);
        }

    }
}
