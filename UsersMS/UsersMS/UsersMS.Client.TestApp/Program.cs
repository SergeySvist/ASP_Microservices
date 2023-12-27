using UsersMS.Client;

using UsersMS.Cache;
using UsersMS.DTO;

var userDTO = new UserDTO() {
    Id = 2,
    Name= "Second Test",
    Email = "second@test.com",
    BirthDate = DateTime.Now.AddYears(-20),
    PhoneNumber = "+380712482134",
    RegisteredAt = DateTime.Now,
};
var cacheClient = new UserCacheClient();

cacheClient.DeleteUser(1);