using DiscountMS.Contracts;
using DiscountMS.Contracts.Enums;
using DiscountMS.Host.Domain.DataLayer;
using DiscountMS.Host.Domain.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            Discount baseDiscountPart = BuildDiscountObjectFromAddDiscountDTO(addInventory);
            InventoryItemDiscount specificDiscountPart = new InventoryItemDiscount() { 
                InventoryItemId = addInventory.InventoryId,
            };

            Tuple<Discount, InventoryItemDiscount> addedDiscount = await _discountDataLayer.AddInventoryItemDiscount(baseDiscountPart, specificDiscountPart);

            InventoryItemDiscountDTO inventoryItemDiscountDTO = BuildInventoryItemDiscountDTOFromDbParts(baseDiscountPart, specificDiscountPart);

            return inventoryItemDiscountDTO;
        }

        private Discount BuildDiscountObjectFromAddDiscountDTO(AddDiscountDTO addDiscount)
        {
            return new Discount()
            {
                DiscountType = new DiscountType { DiscountTypeId = (int)addDiscount.DiscountType },
                DiscountAmountType = new DiscountAmountType { DiscountAmountTypeId = (int)addDiscount.DiscountAmountType },
                DiscountAmount = addDiscount.DiscountAmount,
                DateFrom = addDiscount.DateFrom,
                DateTo = addDiscount.DateTo,
                DiscountTerminationType = new DiscountTerminationType { DiscountTerminationTypeId = (int)addDiscount.DiscountTerminationType },

            };
        }

        public async Task<PersonalDiscountDTO> AddPersonalDiscount(AddPersonalDiscountDTO addPersonal)
        {
            Discount baseDiscountPart = BuildDiscountObjectFromAddDiscountDTO(addPersonal);
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

        private InventoryItemDiscountDTO BuildInventoryItemDiscountDTOFromDbParts(Discount baseDiscountPart, InventoryItemDiscount specificDiscountPart)
        {
            InventoryItemDiscountDTO discountDTO = BuildDiscountDtoBase<InventoryItemDiscountDTO>(baseDiscountPart);
            discountDTO.InventoryItemDiscountId = specificDiscountPart.InventoryItemDiscountId;
            discountDTO.InventoryId = specificDiscountPart.InventoryItemId;

            return discountDTO;
        }

        private T BuildDiscountDtoBase<T>(Discount baseDiscountPart) where T : DiscountDTO
        {
            T result = Activator.CreateInstance<T>();

            result.DiscountId = baseDiscountPart.DiscountId;
            result.DiscountAmountType = (Contracts.Enums.DiscountAmountTypeEnum)baseDiscountPart.DiscountAmountType.DiscountAmountTypeId;
            result.DiscountAmount = baseDiscountPart.DiscountAmount;
            result.DateFrom = baseDiscountPart.DateFrom;
            result.DateTo = baseDiscountPart.DateTo;
            result.TerminationType = (Contracts.Enums.DiscountTerminationTypeEnum)baseDiscountPart.DiscountTerminationType.DiscountTerminationTypeId;

            return result;
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
                PersonalDiscountDTO discountDTO = BuildPersonalDiscountDTOFromDbParts(itemDiscountParts.Item1, itemDiscountParts.Item2);

                activeDiscountDTOs.Add(discountDTO);
            }

            return activeDiscountDTOs.ToArray();
        }
    }
}
