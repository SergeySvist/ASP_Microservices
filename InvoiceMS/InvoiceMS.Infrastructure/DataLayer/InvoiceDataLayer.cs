using InvoiceMS.Infrastructure.Domain.DbCtx;
using InvoiceMS.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.DataLayer
{
    public class InvoiceDataLayer : IInvoiceDataLayer
    {
        public async Task<Invoice> AddInvoice(Invoice invoice, List<InvoiceEntry> newInvoiceEntries)
        {
            using (InvoiceMsDbContext dbContext = new InvoiceMsDbContext())
            {
                IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
                
                try
                {
                    dbContext.Invoices.Add(invoice);
                    await dbContext.SaveChangesAsync();

                    if (invoice.InvoiceId > 0)
                    {
                        foreach (InvoiceEntry entry in newInvoiceEntries)
                        {
                            entry.InvoiceId = invoice.InvoiceId;
                        }

                        dbContext.InvoiceEntries.AddRange(newInvoiceEntries);

                        await dbContext.SaveChangesAsync();

                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }


                return invoice;
            }
        }

        public async Task<bool> DeleteInvoiceById(long id)
        {
            bool isInvoiceDeleted = false;
            using (InvoiceMsDbContext dbContext = new InvoiceMsDbContext())
            {
                Invoice invoiceById = await dbContext.Invoices.Where(i => i.InvoiceId == id).Include(i => i.InvoiceEntries).FirstOrDefaultAsync();

                if (invoiceById != null)
                {
                    IDbContextTransaction transaction = dbContext.Database.BeginTransaction();

                    try
                    {
                        if (invoiceById.InvoiceEntries.Count > 0)
                        {
                            dbContext.InvoiceEntries.RemoveRange(invoiceById.InvoiceEntries);
                        }

                        dbContext.Invoices.Remove(invoiceById);

                        await dbContext.SaveChangesAsync(); 
                        transaction.Commit();
                        
                        isInvoiceDeleted = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        throw;
                    }
                }


                return isInvoiceDeleted;
            }

        }

        public async Task<Invoice> GetInvoiceById(long id)
        {
            using (InvoiceMsDbContext dbContext = new InvoiceMsDbContext())
            {
                Invoice invoiceById = await dbContext.Invoices.Where(i => i.InvoiceId == id).Include(i => i.InvoiceEntries).FirstOrDefaultAsync();

                return invoiceById != null ? invoiceById : new Invoice();
            }
        }

        public async Task<List<Invoice>> GetInvoiceByUserId(long id)
        {
            using (InvoiceMsDbContext dbContext = new InvoiceMsDbContext())
            {
                List<Invoice> invoicesByUserId = new List<Invoice>();

                if (dbContext.Invoices.Any(i => i.UserId == id))
                {
                    invoicesByUserId = await dbContext.Invoices.Where(i => i.UserId == id).Include(i => i.InvoiceEntries).ToListAsync();
                }

                return invoicesByUserId;
            }
        }

        public async Task<List<InvoiceEntry>> GetInvoiceEntriesByInventoryItemId(long itemId)
        {
            using (InvoiceMsDbContext dbContext = new InvoiceMsDbContext())
            {
                return await dbContext.InvoiceEntries.Where(ie => ie.InventoryId == itemId).ToListAsync();
            }
        }

        public Task SaveUpdatedInvoiceEntries(List<InvoiceEntry> invoiceEntriesByInventoryItemId)
        {
            using (InvoiceMsDbContext dbContext = new InvoiceMsDbContext())
            {
                dbContext.InvoiceEntries.UpdateRange(invoiceEntriesByInventoryItemId);

                dbContext.SaveChanges();

                return Task.CompletedTask;
            }
        }
    }
}
