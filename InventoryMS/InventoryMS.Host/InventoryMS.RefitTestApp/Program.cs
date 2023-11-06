using InventoryMS.Client;
using InventoryMS.Contracts;
using InventoryMS.Host.Domain.Models;
//var inventoryMsClient = RestService.For<IInventoryMSClient>("http://localhost:5029/");

//var inventoryItems = await inventoryMsClient.Get();
EditInventoryItemDTO dto = new EditInventoryItemDTO() { Id = 1, Name = "Serg", Price = 101};

InventoryItem item  = new InventoryItem();

Console.WriteLine($"{item.Name} => {item.Price}");
item.UpdateFromEditInventoryItemDto(dto);
Console.WriteLine($"{item.Name} => {item.Price}");