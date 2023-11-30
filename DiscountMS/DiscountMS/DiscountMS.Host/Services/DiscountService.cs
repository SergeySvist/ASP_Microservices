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
            Discount baseDiscountPart = new Discount { 
                DiscountType = new DiscountType { DiscountTypeId = (int)addPersonal.DiscountType },
                DiscountAmountType = new DiscountAmountType { DiscountAmountTypeId = (int)addPersonal.DiscountAmountType },
                DiscountAmount = addPersonal.DiscountAmount,
                DateFrom = addPersonal.DateFrom,
                DateTo = addPersonal.DateTo,
                DiscountTerminationType = new DiscountTerminationType { DiscountTerminationTypeId = (int)addPersonal.DiscountTerminationType },
            };
            PersonalDiscount specificDiscountPart = new PersonalDiscount { 
                UserId = addPersonal.UserId,
            };

            Tuple<Discount, PersonalDiscount> addedDiscount = await _discountDataLayer.AddPersonalDiscount(baseDiscountPart, specificDiscountPart);

            PersonalDiscountDTO addedPersonalDiscountDTO = BuildPersonalDiscountDTOFromDbParts(baseDiscountPart, specificDiscountPart);

            return addedPersonalDiscountDTO;
        }

        private PersonalDiscountDTO BuildPersonalDiscountDTOFromDbParts(Discount baseDiscountPart, PersonalDiscount specificDiscountPart)
        {
            PersonalDiscountDTO discountDTO = new PersonalDiscountDTO()
            {
                DiscountId = baseDiscountPart.DiscountId,
                DiscountAmountType = (Contracts.Enums.DiscountAmountTypeEnum)baseDiscountPart.DiscountAmountType.DiscountAmountTypeId,
                DiscountAmount = baseDiscountPart.DiscountAmount,
                DateFrom = baseDiscountPart.DateFrom,
                DateTo = baseDiscountPart.DateTo,
                TerminationType = (Contracts.Enums.DiscountTerminationTypeEnum)baseDiscountPart.DiscountTerminationType.DiscountTerminationTypeId,
                PersonalDiscountId = specificDiscountPart.PersonalDiscountId,
                UserId = specificDiscountPart.UserId,
            };

            return discountDTO;
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
                    DiscountAmountType = (DiscountAmountTypeEnum)itemDiscountParts.Item1.DiscountAmountType.DiscountAmountTypeId,
                    DiscountAmount = itemDiscountParts.Item1.DiscountAmount,
                    DateFrom = itemDiscountParts.Item1.DateFrom,
                    DateTo = itemDiscountParts.Item1.DateTo,
                    TerminationType = (DiscountTerminationTypeEnum)itemDiscountParts.Item1.DiscountTerminationType.DiscountTerminationTypeId,
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
                    DiscountAmountType = (Contracts.Enums.DiscountAmountTypeEnum)itemDiscountParts.Item1.DiscountAmountType.DiscountAmountTypeId,
                    DiscountAmount = itemDiscountParts.Item1.DiscountAmount,
                    DateFrom = itemDiscountParts.Item1.DateFrom,
                    DateTo = itemDiscountParts.Item1.DateTo,
                    TerminationType = (Contracts.Enums.DiscountTerminationTypeEnum)itemDiscountParts.Item1.DiscountTerminationType.DiscountTerminationTypeId,
                    PersonalDiscountId = itemDiscountParts.Item2.PersonalDiscountId,
                    UserId = itemDiscountParts.Item2.UserId,
                };

                activeDiscountDTOs.Add(discountDTO);
            }

            return activeDiscountDTOs.ToArray();
        }
    }
}
