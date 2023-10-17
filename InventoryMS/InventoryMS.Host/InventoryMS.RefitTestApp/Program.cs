using InventoryMS.Client;

//var inventoryMsClient = RestService.For<IInventoryMSClient>("http://localhost:5029/");

//var inventoryItems = await inventoryMsClient.Get();

var inventoryItems = await InventoryMsClient.Client.Get();
Console.WriteLine();