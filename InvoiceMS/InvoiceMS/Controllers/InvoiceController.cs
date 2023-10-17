using Microsoft.AspNetCore.Mvc;
using InvoiceMS.Contracts;
using UsersMS.Client;
using InventoryMS.Client;
using InvoiceMS.Infrastructure.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("byUser/{id}")]
        public async Task<IEnumerable<InvoiceDTO>> GetByUserId(long id)
        {
            return await _invoiceService.GetInvoicesByUser(id);
        }

        [HttpGet("{id}")]
        public async Task<InvoiceDTO> Get(long id)
        {
            return await _invoiceService.GetInvoiceById(id);
        }
        // POST api/<InvoiceController>
        [HttpPost]
        public async Task<InvoiceDTO> Post([FromBody] AddInvoiceDTO invoiceToAdd)
        {
            return await _invoiceService.AddInvoice(invoiceToAdd);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id)
        {
            //ToDo: удалять только неоплаченные у которых истек срок
            return await _invoiceService.DeleteInvoiceById(id);
        }

    }
}
