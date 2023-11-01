using InventoryMS.Events;

namespace InventoryMS.Host.Services
{
    public interface IEventService
    {
        InventoryMsEvent CreateInventoryItemNameUpdatedEvent(long Id, string OldName, string NewName);
        InventoryMsEvent CreateInventoryItemPriceUpdatedEvent(long Id, decimal OldPrice, decimal NewPrice);
    }
}