using DiscountMS.Contracts;

namespace DiscountMS.Host.Services
{
    public interface IDiscountService
    {
        Task<InventoryItemDiscountDTO> AddInventoryItemDiscount(AddInventoryItemDiscountDTO addInventory);
        Task<PersonalDiscountDTO> AddPersonalDiscount(AddPersonalDiscountDTO addPersonal);
        Task<InventoryItemDiscountDTO[]> GetActiveInventoryItemDiscounts();
        Task<PersonalDiscountDTO[]> GetActivePersonalDiscounts();
    }
}
