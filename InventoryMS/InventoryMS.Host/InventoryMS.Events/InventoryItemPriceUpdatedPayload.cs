using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMS.Events
{
    public class InventoryItemPriceUpdatedPayload
    {
        public long ItemId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

    }
}
