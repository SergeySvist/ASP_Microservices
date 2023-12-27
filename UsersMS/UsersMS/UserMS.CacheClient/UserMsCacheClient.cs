using StackExchange.Redis;
using System.Text.Json;
using UsersMS.DTO;

namespace UserMS.CacheClient
{

    public class UserMSCacheClient : IUserMSCacheClient
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _redisDatabase;

        public UserMSCacheClient()
        {
            _redisConnection = ConnectionMultiplexer.Connect("localhost");
            _redisDatabase = _redisConnection.GetDatabase(0);
        }

        ~UserMSCacheClient()
        {
            _redisConnection.Close();
        }

        public bool AddUserFromUserDTO(UserDTO userDTO)
        {
            bool isUserAdded = false;

            string userJson = JsonSerializer.Serialize(userDTO);
            try
            {
                _redisDatabase.StringSet(BuildUserKey(userDTO.Id), userJson);
            }
            catch (Exception)
            {
                throw;
            }

            return isUserAdded;
        }

        public UserDTO? GetUserById(long id)
        {
            UserDTO? user = null;
            try
            {
                RedisValue userJson = _redisDatabase.StringGet(BuildUserKey(id));

                if (userJson.HasValue)
                {
                    user = JsonSerializer.Deserialize<UserDTO>(userJson);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }

        public bool DeleteUserById(long id)
        {
            bool isUserDeleted = false;

            try
            {
                isUserDeleted = _redisDatabase.KeyDelete(BuildUserKey(id));

            }
            catch (Exception)
            {
                throw;
            }

            return isUserDeleted;

        }

        private RedisKey BuildUserKey(long id)
        {
            return new RedisKey($"User: {id}");
        }

    }
    
}