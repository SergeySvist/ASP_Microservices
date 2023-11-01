using InventoryMS.Events;

namespace InventoryMS.Host.Services
{
    public class EventService : IEventService
    {
        public InventoryMsEvent CreateInventoryItemNameUpdatedEvent(long Id, string OldName, string NewName)
        {
            return new InventoryMsEvent
            {
                EventType = EventType.InventoryItemNameUpdated,
                EventPayload = new InventoryItemNameUpdatedPayload { ItemId = Id, OldName = OldName, NewName = NewName }
            };
        }

        public InventoryMsEvent CreateInventoryItemPriceUpdatedEvent(long Id, decimal OldPrice, decimal NewPrice)
        {
            return new InventoryMsEvent
            {
                EventType = EventType.InventoryItemPriceUpdated,
                EventPayload = new InventoryItemPriceUpdatedPayload { ItemId = Id, OldPrice = OldPrice, NewPrice = NewPrice }
            };
        }
    }
}
