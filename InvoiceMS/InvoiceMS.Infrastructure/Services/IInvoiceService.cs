using InvoiceMS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> AddInvoice(AddInvoiceDTO invoiceToAdd);
        Task<bool> DeleteInvoiceById(long id);
        Task<InvoiceDTO> GetInvoiceById(long id);
        Task<List<InvoiceDTO>> GetInvoicesByUser(long id);
    }
}
