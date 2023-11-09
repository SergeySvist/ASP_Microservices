using InventoryMS.Contracts;
using InventoryMS.Events;
using InventoryMS.Host.Domain.Models;
using InventoryMS.Host.MessageBroker;
using System.Text.Json;

namespace InventoryMS.Host.Services
{
    public class EventService : IEventService
    {
        private readonly IMessageBusProducer _messageBusProducer;

        public EventService(IMessageBusProducer messageBusProducer)
        {
            _messageBusProducer = messageBusProducer;
        }

        public InventoryMsEvent CreateInventoryItemNameUpdatedEvent(long Id, string OldName, string NewName)
        {
            return new InventoryMsEvent
            {
                EventType = EventType.InventoryItemNameUpdated,
                EventPayload = JsonSerializer.Serialize(new InventoryItemNameUpdatedPayload { ItemId = Id, OldName = OldName, NewName = NewName })
            };
        }

        public InventoryMsEvent CreateInventoryItemPriceUpdatedEvent(long Id, decimal OldPrice, decimal NewPrice)
        {
            return new InventoryMsEvent
            {
                EventType = EventType.InventoryItemPriceUpdated,
                EventPayload = JsonSerializer.Serialize( new InventoryItemPriceUpdatedPayload { ItemId = Id, OldPrice = OldPrice, NewPrice = NewPrice })
            };
        }

        public Task<bool> ProcessAndPublishInventoryItemUpdates(InventoryItem itemBeforeUpdate, EditInventoryItemDTO inventoryItemUpdates)
        {
            try
            {
                List<InventoryMsEvent> eventsToPublish = new List<InventoryMsEvent>();

                if (inventoryItemUpdates.Name != null && itemBeforeUpdate.Name != inventoryItemUpdates.Name)
                {
                    eventsToPublish.Add(this.CreateInventoryItemNameUpdatedEvent(inventoryItemUpdates.Id, itemBeforeUpdate.Name, inventoryItemUpdates.Name));
                }
                if (inventoryItemUpdates.Price.HasValue && itemBeforeUpdate.Price != inventoryItemUpdates.Price)
                {
                    eventsToPublish.Add(this.CreateInventoryItemPriceUpdatedEvent(inventoryItemUpdates.Id, itemBeforeUpdate.Price, (decimal)inventoryItemUpdates.Price));
                }

                if (eventsToPublish.Count > 0)
                {
                    Parallel.ForEach(eventsToPublish, (evnt) =>
                    {
                        _messageBusProducer.PublishEvent(evnt);
                    });
                }

                return Task.FromResult(true);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
