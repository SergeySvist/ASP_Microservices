using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InventoryMS.Contracts;
using Refit;

namespace InventoryMS.Client
{
    public interface IInventoryMsClient
    {
        [Get("/api/Inventory")]
        public Task<List<InventoryItemDTO>> Get();

        [Post("/api/Inventory/searchByIds")]
        public Task<List<InventoryItemDTO>> SearchByIds([Body] long[] ids);

    }
}
