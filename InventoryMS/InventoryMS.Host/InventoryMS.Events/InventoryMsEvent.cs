namespace InventoryMS.Events
{
    public class InventoryMsEvent
    {
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public EventType EventType { get; set; }

        public object EventPayload { get; set; }
    }
}