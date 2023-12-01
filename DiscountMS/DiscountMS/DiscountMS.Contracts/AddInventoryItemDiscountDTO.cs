using DiscountMS.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts
{
    public class AddInventoryItemDiscountDTO: AddDiscountDTO
    {
        public AddInventoryItemDiscountDTO() {
            DiscountType = DiscountTypeEnum.InventoryItem;
        }
        public long InventoryId { get; set; }

    }
}
