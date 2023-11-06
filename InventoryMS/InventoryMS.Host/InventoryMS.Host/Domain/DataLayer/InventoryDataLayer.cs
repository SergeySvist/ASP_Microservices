using InventoryMS.Host.Domain.DbCtx;
using InventoryMS.Host.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Host.Domain.DataLayer
{
    public class InventoryDataLayer : IInventoryDataLayer
    {
        public async Task<InventoryItem> Add(InventoryItem inventoryItem)
        {
            using (InventoryMsDbContext db = new InventoryMsDbContext())
            {
                db.InventoryItems.Add(inventoryItem);
                await db.SaveChangesAsync();

                return inventoryItem;
            }
        }

        public async Task<List<InventoryItem>> GetAll()
        {
            using (InventoryMsDbContext db = new InventoryMsDbContext())
            {
                return await db.InventoryItems.ToListAsync();
            }
        }

        public async Task<List<InventoryItem>> GetByIds(long[] ids)
        {
            using (InventoryMsDbContext db = new InventoryMsDbContext())
            {
                var searchResult = db.InventoryItems.Where(i => ids.Contains(i.Id));
                if (searchResult.Any())
                    return await searchResult.ToListAsync();
                return new List<InventoryItem>();
            }

        }

        public async Task<InventoryItem> GetById(long id)
        {
            using (InventoryMsDbContext db = new InventoryMsDbContext())
            {
                InventoryItem? inventoryItemById = await db.InventoryItems.FirstOrDefaultAsync(i => i.Id == id);

                return inventoryItemById == null ? inventoryItemById : new InventoryItem();
            }
        }

        public async Task<bool> Edit(InventoryItem updatedInventoryItem)
        {
            using (InventoryMsDbContext db = new InventoryMsDbContext())
            {
                int rowsUpdated = 0;
                InventoryItem? inventoryItemToUpdate = await db.InventoryItems.FirstOrDefaultAsync(i => i.Id == updatedInventoryItem.Id);

                if (inventoryItemToUpdate != null)
                {
                    inventoryItemToUpdate.Name = updatedInventoryItem.Name;
                    inventoryItemToUpdate.Price = updatedInventoryItem.Price;

                    await db.SaveChangesAsync();
                }

                return rowsUpdated > 0;
            }
        }
    }
}
