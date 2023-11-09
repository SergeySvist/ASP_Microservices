using InventoryMS.Events;

namespace InvoiceMS.Infrastructure
{
    public interface IInventoryMsEventsProcessor
    {
        Task<bool> ProcessEvent(InventoryMsEvent eventReceived);
    }
}