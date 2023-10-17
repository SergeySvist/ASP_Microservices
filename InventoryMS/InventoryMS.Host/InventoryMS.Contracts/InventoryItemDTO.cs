using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMS.Contracts
{
    public class InventoryItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
