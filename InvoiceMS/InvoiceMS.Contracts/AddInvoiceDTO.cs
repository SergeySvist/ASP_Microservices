using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Contracts
{
    public class AddInvoiceDTO
    {
        public long UserId { get; set; }
        public AddInvoiceEntry[] InvoiceEntries { get; set; }
    }
}
