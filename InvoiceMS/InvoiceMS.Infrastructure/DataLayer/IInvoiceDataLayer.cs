using InvoiceMS.Infrastructure.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.DataLayer
{
    public interface IInvoiceDataLayer
    {
        Task<Invoice> AddInvoice(Invoice invoice, List<InvoiceEntry> newInvoiceEntries);
        Task<bool> DeleteInvoiceById(long id);
        Task<Invoice> GetInvoiceById(long id);
        Task<List<Invoice>> GetInvoiceByUserId(long id);
    }
}
