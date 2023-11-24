using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts.Enums
{
    public enum DiscountTypeEnum
    {
        Personal = 1,
        InventoryItem = 2,
        FromInvoiceTotal = 3,
        Sale = 4,
        InventoryItemUponInvoiceAmount = 5,
    }
}
