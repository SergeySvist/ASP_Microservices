using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Contracts
{
    public class AddInvoiceEntry
    {
        public long InventoryId { get; set; }
        public int Amount { get; set; }
    }
}
