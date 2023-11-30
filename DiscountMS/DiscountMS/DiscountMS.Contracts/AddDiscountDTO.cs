using DiscountMS.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts
{
    public abstract class AddDiscountDTO
    {
        public DiscountTypeEnum DiscountType { get; set; }
        public DiscountAmountTypeEnum DiscountAmountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DiscountTerminationTypeEnum DiscountTerminationType { get; set; }

    }
}
