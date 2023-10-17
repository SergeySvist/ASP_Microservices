using Microsoft.AspNetCore.Mvc;
using InventoryMS.Contracts;
using InventoryMS.Host.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        // GET: api/<InventoryController>
        [HttpGet]
        public async Task<List<InventoryItemDTO>> Get()
        {
            return await _inventoryService.GetAll(); ;
        }

        // POST api/<InventoryController>
        [HttpPost]
        public async Task<InventoryItemDTO> Post([FromBody] AddInventoryItemDTO inventoryItemToAdd)
        {
            return await _inventoryService.AddInventoryItem(inventoryItemToAdd); ;
        }

        // POST api/<InventoryController>/searchByIds
        [HttpPost("searchByIds")]
        public async Task<List<InventoryItemDTO>> SearchByIds([FromBody] long[] ids)
        {
            return await _inventoryService.GetByIds(ids); ;
        }

    }
}
