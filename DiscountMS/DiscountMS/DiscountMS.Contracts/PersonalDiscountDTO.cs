using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts
{
    public class PersonalDiscountDTO : DiscountDTO
    {
        public long? PersonalDiscountId { get; set; }
        public long? UserId { get; set; }

        public PersonalDiscountDTO() {
            DiscountType = Enums.DiscountTypeEnum.Personal;
        }
    }
}
