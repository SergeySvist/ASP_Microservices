using InventoryMS.Events;

namespace InventoryMS.Host.MessageBroker
{
    public interface IMessageBusProducer
    {
        bool PublishEvent(InventoryMsEvent eventToPublish);
    }
}