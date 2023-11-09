using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.EventProcessors
{
    public class InventoryItemPriceUpdatedNotification
    {
        public long ItemId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

    }
}
