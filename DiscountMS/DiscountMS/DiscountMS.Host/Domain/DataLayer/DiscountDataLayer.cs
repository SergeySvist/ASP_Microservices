using DiscountMS.Contracts;
using DiscountMS.Host.Domain.DbCtx;
using DiscountMS.Host.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DiscountMS.Host.Domain.DataLayer
{
    public class DiscountDataLayer : IDiscountDataLayer
    {
        public async Task<Tuple<Discount, InventoryItemDiscount>> AddInventoryItemDiscount(Discount baseDiscountPart, InventoryItemDiscount specificDiscountPart)
        {
            using (DiscountServiceDbContext dbContext = new DiscountServiceDbContext())
            {
                using (IDbContextTransaction addDiscountTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        dbContext.InventoryItemDiscounts.Add(specificDiscountPart);
                        await dbContext.SaveChangesAsync();

                        if (specificDiscountPart.InventoryItemDiscountId > 0)
                        {
                            DiscountType? discountTypeById = dbContext.DiscountTypes.Where(dt => dt.DiscountTypeId == baseDiscountPart.DiscountType.DiscountTypeId).FirstOrDefault();
                            DiscountAmountType? discountAmountTypeById = dbContext.DiscountAmountTypes.Where(dt => dt.DiscountAmountTypeId == baseDiscountPart.DiscountAmountType.DiscountAmountTypeId).FirstOrDefault();
                            DiscountTerminationType? discountTerminationTypeById = dbContext.DiscountTerminationTypes.Where(dt => dt.DiscountTerminationTypeId == baseDiscountPart.DiscountTerminationType.DiscountTerminationTypeId).FirstOrDefault();

                            baseDiscountPart.DiscountType = discountTypeById;
                            baseDiscountPart.DiscountAmountType = discountAmountTypeById;
                            baseDiscountPart.DiscountTerminationType = discountTerminationTypeById;

                            baseDiscountPart.SpecificDiscountTableKey = specificDiscountPart.InventoryItemDiscountId;

                            dbContext.Discounts.Add(baseDiscountPart);
                            await dbContext.SaveChangesAsync();
                        }

                        addDiscountTransaction.Commit();

                        return new Tuple<Discount, InventoryItemDiscount>(baseDiscountPart, specificDiscountPart);
                    }
                    catch (Exception)
                    {
                        addDiscountTransaction.Rollback();
                        throw;
                    }
                }

            }
        }

        //Note: sould be used outside DataLayer only
        public Task<Tuple<DiscountType?, DiscountAmountType?, DiscountTerminationType?>> GetDiscountCategories(int discountTypeId, int discountAmountTypeId, int discountTerminationTypeId)
        {
            using (DiscountServiceDbContext dbContext = new DiscountServiceDbContext())
            {
                DiscountType? discountTypeById = dbContext.DiscountTypes.Where(dt => dt.DiscountTypeId == discountTypeId).FirstOrDefault();
                DiscountAmountType? discountAmountTypeById = dbContext.DiscountAmountTypes.Where(dt => dt.DiscountAmountTypeId == discountAmountTypeId).FirstOrDefault();
                DiscountTerminationType? discountTerminationTypeById = dbContext.DiscountTerminationTypes.Where(dt => dt.DiscountTerminationTypeId == discountTerminationTypeId).FirstOrDefault();
                

                return Task.FromResult( new Tuple<DiscountType?, DiscountAmountType?, DiscountTerminationType?>(discountTypeById, discountAmountTypeById, discountTerminationTypeById));
            }
        }

        public async Task<Tuple<Discount, PersonalDiscount>> AddPersonalDiscount(Discount baseDiscountPart, PersonalDiscount specificDiscountPart)
        {
            using (DiscountServiceDbContext dbContext = new DiscountServiceDbContext())
            {
                using (IDbContextTransaction addDiscountTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        dbContext.PersonalDiscounts.Add(specificDiscountPart);
                        await dbContext.SaveChangesAsync();

                        if(specificDiscountPart.PersonalDiscountId > 0)
                        {
                            DiscountType? discountTypeById = dbContext.DiscountTypes.Where(dt => dt.DiscountTypeId == baseDiscountPart.DiscountType.DiscountTypeId).FirstOrDefault();
                            DiscountAmountType? discountAmountTypeById = dbContext.DiscountAmountTypes.Where(dt => dt.DiscountAmountTypeId == baseDiscountPart.DiscountAmountType.DiscountAmountTypeId).FirstOrDefault();
                            DiscountTerminationType? discountTerminationTypeById = dbContext.DiscountTerminationTypes.Where(dt => dt.DiscountTerminationTypeId == baseDiscountPart.DiscountTerminationType.DiscountTerminationTypeId).FirstOrDefault();

                            baseDiscountPart.DiscountType = discountTypeById;
                            baseDiscountPart.DiscountAmountType = discountAmountTypeById;
                            baseDiscountPart.DiscountTerminationType = discountTerminationTypeById;

                            baseDiscountPart.SpecificDiscountTableKey = specificDiscountPart.PersonalDiscountId;

                            dbContext.Discounts.Add(baseDiscountPart);
                            await dbContext.SaveChangesAsync();
                        }

                        addDiscountTransaction.Commit();

                        return new Tuple<Discount, PersonalDiscount>(baseDiscountPart, specificDiscountPart);
                    }
                    catch (Exception)
                    {
                        addDiscountTransaction.Rollback();
                        throw;
                    }
                }

            }
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
