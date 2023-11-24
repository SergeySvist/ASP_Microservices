using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts
{
    public class InventoryItemDiscountDTO : DiscountDTO
    {
        public long? InventoryItemDiscountId { get; set; }
        public long? InventoryId { get; set; }

        public InventoryItemDiscountDTO()
        {
            DiscountType = Enums.DiscountTypeEnum.InventoryItem;
        }

    }
}
