using DiscountMS.Contracts;
using DiscountMS.Contracts.Enums;
using DiscountMS.Host.Domain.DataLayer;
using DiscountMS.Host.Domain.Model;

namespace DiscountMS.Host.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountDataLayer _discountDataLayer;
        public DiscountService(IDiscountDataLayer discountDataLayer)
        {
            _discountDataLayer = discountDataLayer;
        }

        public async Task<InventoryItemDiscountDTO> AddInventoryItemDiscount(AddInventoryItemDiscountDTO addInventory)
        {
            Discount baseDiscountPart = new Discount();
            InventoryItemDiscount specificDiscountPart = new InventoryItemDiscount();

            Tuple<Discount, InventoryItemDiscount> addedDiscount = await _discountDataLayer.AddInventoryItemDiscount(baseDiscountPart, specificDiscountPart);

            return null;
        }

        public async Task<PersonalDiscountDTO> AddPersonalDiscount(AddPersonalDiscountDTO addPersonal)
        {
            Discount baseDiscountPart = new Discount();
            PersonalDiscount specificDiscountPart = new PersonalDiscount();

            Tuple<Discount, PersonalDiscount> addedDiscount = await _discountDataLayer.AddPersonalDiscount(baseDiscountPart, specificDiscountPart);

            return null;
        }

        public async Task<InventoryItemDiscountDTO[]> GetActiveInventoryItemDiscounts()
        {
            Tuple<Discount, InventoryItemDiscount>[] inventoryItemDiscountParts = await _discountDataLayer.GetAllActiveInventoryItemDiscounts();
            
            List<InventoryItemDiscountDTO> activeDiscountDTOs = new List<InventoryItemDiscountDTO>();

            foreach (Tuple<Discount, InventoryItemDiscount> itemDiscountParts in inventoryItemDiscountParts)
            {
                InventoryItemDiscountDTO discountDTO = new InventoryItemDiscountDTO()
                {
                    DiscountId = itemDiscountParts.Item1.DiscountId,
                    DiscountAmountType = (Contracts.Enums.DiscountAmountType)itemDiscountParts.Item1.DiscountAmountType.DiscountAmountTypeId,
                    DiscountAmount = itemDiscountParts.Item1.DiscountAmount,
                    DateFrom = itemDiscountParts.Item1.DateFrom,
                    DateTo = itemDiscountParts.Item1.DateTo,
                    TerminationType = (Contracts.Enums.DiscountTerminationType)itemDiscountParts.Item1.DiscountTerminationType.DiscountTerminationTypeId,
                    InventoryItemDiscountId = itemDiscountParts.Item2.InventoryItemDiscountId,
                    InventoryId = itemDiscountParts.Item2.InventoryItemId,
                };

                activeDiscountDTOs.Add(discountDTO);
            }

            return activeDiscountDTOs.ToArray();
            
        }

        public async Task<PersonalDiscountDTO[]> GetActivePersonalDiscounts()
        {
            Tuple<Discount, PersonalDiscount>[] personalDiscountParts = await _discountDataLayer.GetAllActivePersonalDiscounts();

            List<PersonalDiscountDTO> activeDiscountDTOs = new List<PersonalDiscountDTO>();

            foreach (Tuple<Discount, PersonalDiscount> itemDiscountParts in personalDiscountParts)
            {
                PersonalDiscountDTO discountDTO = new PersonalDiscountDTO()
                {
                    DiscountId = itemDiscountParts.Item1.DiscountId,
                    DiscountAmountType = (Contracts.Enums.DiscountAmountType)itemDiscountParts.Item1.DiscountAmountType.DiscountAmountTypeId,
                    DiscountAmount = itemDiscountParts.Item1.DiscountAmount,
                    DateFrom = itemDiscountParts.Item1.DateFrom,
                    DateTo = itemDiscountParts.Item1.DateTo,
                    TerminationType = (Contracts.Enums.DiscountTerminationType)itemDiscountParts.Item1.DiscountTerminationType.DiscountTerminationTypeId,
                    PersonalDiscountId = itemDiscountParts.Item2.PersonalDiscountId,
                    UserId = itemDiscountParts.Item2.UserId,
                };

                activeDiscountDTOs.Add(discountDTO);
            }

            return activeDiscountDTOs.ToArray();
        }
    }
}
