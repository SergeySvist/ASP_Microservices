using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Contracts
{
    public class InvoiceEntryDTO
    {
        public long Id { get; set; }
        public long InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price{ get; set; }
        public int Amount { get; set; }
        public decimal RowTotal { get { return Price * Amount; } }
    }
}
