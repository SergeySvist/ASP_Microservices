using InventoryMS.Contracts;
using InventoryMS.Host.Domain.Models;

namespace InventoryMS.Host
{
    public interface IInventoryDataLayer
    {
        Task<InventoryItem> Add(InventoryItem inventoryItem);
        Task<List<InventoryItem>> GetAll();
        Task<List<InventoryItem>> GetByIds(long[] ids);
    }
}