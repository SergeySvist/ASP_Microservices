using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.EventProcessors
{
    public class InventoryItemNameUpdatedNotification
    {
        public long ItemId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }

    }
}
