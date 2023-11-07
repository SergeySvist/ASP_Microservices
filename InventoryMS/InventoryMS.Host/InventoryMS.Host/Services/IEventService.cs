using InventoryMS.Contracts;
using InventoryMS.Events;
using InventoryMS.Host.Domain.Models;

namespace InventoryMS.Host.Services
{
    public interface IEventService
    {
        InventoryMsEvent CreateInventoryItemNameUpdatedEvent(long Id, string OldName, string NewName);
        InventoryMsEvent CreateInventoryItemPriceUpdatedEvent(long Id, decimal OldPrice, decimal NewPrice);
        Task<bool> ProcessAndPublishInventoryItemUpdates(InventoryItem itemBeforeUpdate, EditInventoryItemDTO inventoryItemUpdates);
    }
}