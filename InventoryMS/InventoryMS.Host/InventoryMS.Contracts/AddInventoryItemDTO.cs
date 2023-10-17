using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMS.Contracts
{
    public class AddInventoryItemDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
