namespace InventoryMS.Events
{
    public class InventoryMsEvent
    {
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public EventType EventType { get; set; }

        public string EventPayload { get; set; }
    }
}