using InvoiceMS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.DTO;
using UsersMS.Client;
using InventoryMS.Client;
using InventoryMS.Contracts;
using InvoiceMS.Infrastructure.Domain.Entities;
using InvoiceMS.Infrastructure.DataLayer;
using Mapster;
using InvoiceMS.Infrastructure.EventProcessors;

namespace InvoiceMS.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService, IInventoryItemUpdateNotificationsProcessor
    {
        private readonly IUserMsClient _userMsClient;
        private readonly IInventoryMsClient _inventoryMsClient;
        private readonly IInvoiceDataLayer _invoiceDataLayer;
        public InvoiceService(IUserMsClient userMsClient, IInvoiceDataLayer invoiceDataLayer)
        {
            _userMsClient = userMsClient;
            _inventoryMsClient = InventoryMsClient.Client;
            _invoiceDataLayer = invoiceDataLayer;
        }

        public async Task<InvoiceDTO> AddInvoice(AddInvoiceDTO addInvoiceDTO)
        {
            UserDTO userById = await _userMsClient.GetUserByID(addInvoiceDTO.UserId);
            InvoiceDTO addedInvoiceDTO = new InvoiceDTO();

            if (userById.Id > 0 && addInvoiceDTO.InvoiceEntries.Length > 0)
            {
                List<InventoryItemDTO> inventoryItemDTOs = await _inventoryMsClient.SearchByIds(addInvoiceDTO.InvoiceEntries.Select(e => e.InventoryId).ToArray());

                if(inventoryItemDTOs.Count > 0)
                {
                    ILookup<long, InventoryItemDTO> inventoryItemLookup = inventoryItemDTOs.ToLookup(i => i.Id);
                    
                    List<InvoiceEntry> newInvoiceEntries = new List<InvoiceEntry>();

                    foreach(AddInvoiceEntry invoiceEntry in addInvoiceDTO.InvoiceEntries)
                    {

                        var inventoryItemToAdd = inventoryItemLookup[invoiceEntry.InventoryId].FirstOrDefault();

                        if (inventoryItemToAdd != null)
                        {
                            newInvoiceEntries.Add(new InvoiceEntry
                            {
                                InventoryId = inventoryItemToAdd.Id,
                                Name = inventoryItemToAdd.Name,
                                Price = inventoryItemToAdd.Price,
                                Amount = invoiceEntry.Amount,
                            });
                        }
                    }

                    if(newInvoiceEntries.Count > 0)
                    {
                        Invoice invoice = new Invoice { 
                            InvoiceNumber = $"{addInvoiceDTO.UserId}-{DateTime.Now.ToString("yyyy-MM-dd-hh-mm")}",
                            UserId = addInvoiceDTO.UserId ,
                            IssueDate = DateTime.Now,
                            ExpirationDate = DateTime.Now.AddDays(3),

                        };

                        Invoice addedInvoice = await _invoiceDataLayer.AddInvoice(invoice, newInvoiceEntries);

                        addedInvoiceDTO = addedInvoice.Adapt<InvoiceDTO>();
                    }
                }
            }

            return addedInvoiceDTO;
        }

        public async Task<bool> DeleteInvoiceById(long id)
        {
            return await _invoiceDataLayer.DeleteInvoiceById(id);
        }

        public async Task<InvoiceDTO> GetInvoiceById(long id)
        {
            Invoice invoiceById = await _invoiceDataLayer.GetInvoiceById(id);
            return invoiceById.Adapt<InvoiceDTO>();
        }

        public async Task<List<InvoiceDTO>> GetInvoicesByUser(long id)
        {
            List<Invoice> invoicesByUserId = await _invoiceDataLayer.GetInvoiceByUserId(id);

            return invoicesByUserId.Adapt<List<InvoiceDTO>>();
        }

        public async Task ProcessInventoryItemNameUpdatedNotification(InventoryItemNameUpdatedNotification updateNotification)
        {
            List<InvoiceEntry> invoiceEntriesByInventoryItemId = await _invoiceDataLayer.GetInvoiceEntriesByInventoryItemId(updateNotification.ItemId);

            foreach(InvoiceEntry entry in invoiceEntriesByInventoryItemId)
            {
                entry.Name = updateNotification.NewName;
            }

            await _invoiceDataLayer.SaveUpdatedInvoiceEntries(invoiceEntriesByInventoryItemId);
        }

        public async Task ProcessInventoryItemPriceUpdatedNotification(InventoryItemPriceUpdatedNotification updateNotification)
        {
            List<InvoiceEntry> invoiceEntriesByInventoryItemId = await _invoiceDataLayer.GetInvoiceEntriesByInventoryItemId(updateNotification.ItemId);

            foreach (InvoiceEntry entry in invoiceEntriesByInventoryItemId)
            {
                entry.Price = updateNotification.NewPrice;
            }

            await _invoiceDataLayer.SaveUpdatedInvoiceEntries(invoiceEntriesByInventoryItemId);

        }
    }
}
