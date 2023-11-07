using InventoryMS.Client;
using InventoryMS.Contracts;
using InventoryMS.Host.Domain.Models;
//var inventoryMsClient = RestService.For<IInventoryMSClient>("http://localhost:5029/");

//var inventoryItems = await inventoryMsClient.Get();
EditInventoryItemDTO dto = new EditInventoryItemDTO() { Id = 1 };

var list = dto.GetProperties();

foreach (var item in list)
{
    Console.WriteLine(item);
}