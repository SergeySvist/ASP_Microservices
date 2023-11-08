using InventoryMS.Client;
using InventoryMS.Contracts;
using InventoryMS.Host.Domain.Models;
//var inventoryMsClient = RestService.For<IInventoryMSClient>("http://localhost:5029/");

//var inventoryItems = await inventoryMsClient.Get();
//EditInventoryItemDTO dto = new EditInventoryItemDTO() { Id = 1 };

//var list = dto.GetProperties();

//foreach (var item in list)
//{
//    Console.WriteLine(item);
//}

Console.WriteLine("Start main thread");

Thread thread = new Thread(() => {
    Console.WriteLine("Start second thread");
    Thread.Sleep(2000);
    Console.WriteLine("End second thread");

});

thread.IsBackground = false;
thread.Start();

Console.WriteLine("End main thread");