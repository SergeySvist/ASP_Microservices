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
    }
}
