using Microsoft.Extensions.Configuration;
using Refit;
using System.Net.Http;

namespace InventoryMS.Client
{
    public class InventoryMsClient
    {
        public static readonly IInventoryMsClient Client;

        private static readonly string? serviceAddress;

        static InventoryMsClient()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("inventoryclientsettings.json")
            .Build();

            serviceAddress = configuration.GetSection("InventoryServiceAddress").Value;

            Client = RestService.For<IInventoryMsClient>(serviceAddress);
        }

    }
}