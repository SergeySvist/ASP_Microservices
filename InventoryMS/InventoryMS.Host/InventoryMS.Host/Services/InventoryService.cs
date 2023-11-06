using InventoryMS.Contracts;
using InventoryMS.Host.Domain.Models;
using Mapster;

namespace InventoryMS.Host.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryDataLayer _inventoryDataLayer;
        private readonly IEventService _eventService;
        public InventoryService(IInventoryDataLayer inventoryDataLayer, IEventService eventService)
        {
            _inventoryDataLayer = inventoryDataLayer;
            _eventService = eventService;
        }

        public async Task<InventoryItemDTO> AddInventoryItem(AddInventoryItemDTO inventoryItemToAdd)
        {
            InventoryItem inventoryItem = await _inventoryDataLayer.Add(inventoryItemToAdd.Adapt<InventoryItem>());
            return inventoryItem.Adapt<InventoryItemDTO>();
        }

        public async Task<bool> EditInventoryItem(EditInventoryItemDTO inventoryItemToEdit)
        {
            if(inventoryItemToEdit.IsUpdated())
            {
                if(inventoryItemToEdit.Id > 0)
                {
                    InventoryItem inventoryItemById = await _inventoryDataLayer.GetById(inventoryItemToEdit.Id);

                    if(inventoryItemById != null)
                    {
                        inventoryItemById.UpdateFromEditInventoryItemDto(inventoryItemToEdit);
                        
                        return await _inventoryDataLayer.Edit(inventoryItemById);
                    }
                }
            }

            return false;
        }

        public async Task<List<InventoryItemDTO>> GetAll()
        {
            List<InventoryItem> inventoryItems = await _inventoryDataLayer.GetAll();

            return inventoryItems.Adapt<List<InventoryItemDTO>>();
        }

        public async Task<List<InventoryItemDTO>> GetByIds(long[] ids)
        {
            List<InventoryItem> inventoryItems = await _inventoryDataLayer.GetByIds(ids);

            return inventoryItems.Adapt<List<InventoryItemDTO>>();

        }
    }
}
