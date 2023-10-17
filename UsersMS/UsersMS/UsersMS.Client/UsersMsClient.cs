using Microsoft.Extensions.Configuration;
using UsersMS.DTO;
using System.Net.Http;
using System.Text.Json;

namespace UsersMS.Client
{
    public class UsersMsClient : IUserMsClient
    {
        private readonly string? _serviceAddress;
        private readonly string? _apiBaseAddress;
        private bool _configurationReady;
        private readonly HttpClient _httpClient;

        public UsersMsClient() 
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("clientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("UserServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("UserServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
        }
        public Task<UserDTO> CreateUser(AddUserDTO userToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByID(long id)
        {
            UserDTO userById = new UserDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/{id}";

                HttpResponseMessage userMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonUser = await userMsResponce.Content.ReadAsStringAsync();

                userById = JsonSerializer.Deserialize<UserDTO>(jsonUser);
                return userById;
            }

            return userById;
        }
    }
}