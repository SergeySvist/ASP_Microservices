using DiscountMS.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts
{
    public class AddInventoryItemDiscountDTO
    {
        public DiscountAmountType DiscountAmountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DiscountTerminationType TerminationType { get; set; }

        public long InventoryId { get; set; }

    }
}
