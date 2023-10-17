using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryMS.Contracts;

namespace InventoryMS.RefitTestApp
{
    internal interface IInventoryMSClient
    {
        [Get("/api/Inventory")]
        public Task<List<InventoryItemDTO>> Get();

        [Post("/api/Inventory/searchByIds")]
        public Task<List<InventoryItemDTO>> SearchByIds([Body] long[] ids);

    }
}
