﻿using DiscountMS.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountMS.Contracts
{
    public class AddPersonalDiscountDTO : AddDiscountDTO
    {
        public AddPersonalDiscountDTO()
        {
            DiscountType = DiscountTypeEnum.Personal;
        }

        public long UserId { get; set; }

    }
}
