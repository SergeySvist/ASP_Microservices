using DiscountMS.Contracts;
using DiscountMS.Host.Domain.DbCtx;
using DiscountMS.Host.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DiscountMS.Host.Domain.DataLayer
{
    public class DiscountDataLayer : IDiscountDataLayer
    {
        public Task<Tuple<Discount, InventoryItemDiscount>> AddInventoryItemDiscount(Discount baseDiscountPart, InventoryItemDiscount specificDiscountPart)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Discount, PersonalDiscount>> AddPersonalDiscount(Discount baseDiscountPart, PersonalDiscount specificDiscountPart)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<Discount, InventoryItemDiscount?>[]> GetAllActiveInventoryItemDiscounts()
        {
            List<Tuple<Discount, InventoryItemDiscount?>> activeDiscounts = new List<Tuple<Discount, InventoryItemDiscount?>> ();

            using (DiscountServiceDbContext dbContext = new DiscountServiceDbContext())
            {
                List<Discount> discountBaseParts 
                    = dbContext.Discounts.Where(dt => dt.DiscountType.DiscountTypeId == 2)
                    .Include(dat => dat.DiscountAmountType)
                    .Include(dt => dt.DiscountTerminationType)
                    .Include(d => d.DiscountType)
                    .ToList();

                if(discountBaseParts.Count > 0)
                {
                    long[] inventoryDiscountKeys = discountBaseParts.Select(d => d.SpecificDiscountTableKey).ToArray();

                    ILookup<long, InventoryItemDiscount?> inventoryItemDiscountSpecificParts
                        = dbContext.InventoryItemDiscounts.Where(d => inventoryDiscountKeys.Contains(d.InventoryItemDiscountId)).ToLookup(d => d.InventoryItemDiscountId);
                    
                    foreach(Discount discountBasePart in discountBaseParts)
                    {
                        InventoryItemDiscount inventoryItemDiscountSpecificPart = inventoryItemDiscountSpecificParts[discountBasePart.SpecificDiscountTableKey].FirstOrDefault();
                        
                        activeDiscounts.Add(new Tuple<Discount, InventoryItemDiscount?>(discountBasePart, inventoryItemDiscountSpecificPart));
                    }
                }
            }

            return activeDiscounts.ToArray();
        }

        public async Task<Tuple<Discount, PersonalDiscount>[]> GetAllActivePersonalDiscounts()
        {
            List<Tuple<Discount, PersonalDiscount?>> activeDiscounts = new List<Tuple<Discount, PersonalDiscount?>>();

            using (DiscountServiceDbContext dbContext = new DiscountServiceDbContext())
            {
                List<Discount> discountBaseParts
                    = dbContext.Discounts.Where(dt => dt.DiscountType.DiscountTypeId == 1)
                    .Include(dat => dat.DiscountAmountType)
                    .Include(dt => dt.DiscountTerminationType)
                    .Include(d => d.DiscountType)
                    .ToList();

                if (discountBaseParts.Count > 0)
                {
                    long[] personalDiscountKeys = discountBaseParts.Select(d => d.SpecificDiscountTableKey).ToArray();

                    ILookup<long, PersonalDiscount?> personalDiscountSpecificParts
                        = dbContext.PersonalDiscounts.Where(d => personalDiscountKeys.Contains(d.PersonalDiscountId)).ToLookup(d => d.PersonalDiscountId);

                    foreach (Discount discountBasePart in discountBaseParts)
                    {
                        PersonalDiscount personalDiscountSpecificPart = personalDiscountSpecificParts[discountBasePart.SpecificDiscountTableKey].FirstOrDefault();

                        activeDiscounts.Add(new Tuple<Discount, PersonalDiscount?>(discountBasePart, personalDiscountSpecificPart));
                    }
                }
            }

            return activeDiscounts.ToArray();
        }
    }
}
