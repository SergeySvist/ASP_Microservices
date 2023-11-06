using InventoryMS.Contracts;

namespace InventoryMS.Host.Services
{
    public interface IInventoryService
    {
        Task<InventoryItemDTO> AddInventoryItem(AddInventoryItemDTO inventoryItemToAdd);
        Task<bool> EditInventoryItem(EditInventoryItemDTO inventoryItemToEdit);
        Task<List<InventoryItemDTO>> GetAll();
        Task<List<InventoryItemDTO>> GetByIds(long[] ids);
    }
}
