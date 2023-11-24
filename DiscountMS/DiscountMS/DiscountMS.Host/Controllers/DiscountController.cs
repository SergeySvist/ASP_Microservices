using DiscountMS.Contracts;
using DiscountMS.Host.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiscountMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }


        // GET: api/<DiscountController>
        [HttpGet("personal")]
        public async Task<PersonalDiscountDTO[]> GetPersonalDiscounts()
        {
            PersonalDiscountDTO[] personalDiscount = await _discountService.GetActivePersonalDiscounts();

            return personalDiscount;
        }

        // GET: api/<DiscountController>
        [HttpGet("inventory_item")]
        public async Task<InventoryItemDiscountDTO[]> GetInventoryItemDiscounts()
        {
            InventoryItemDiscountDTO[] inventoryDiscount = await _discountService.GetActiveInventoryItemDiscounts();

            return inventoryDiscount;
        }
        // GET api/<DiscountController>/5
        //[HttpGet("{id}")]
        //public DiscountDTO Get(int id)
        //{
        //    return null;
        //}

        // POST api/<DiscountController>
        [HttpPost("personal")]
        public async Task<PersonalDiscountDTO> AddPersonalDiscount([FromBody] AddPersonalDiscountDTO addPersonal)
        {
            PersonalDiscountDTO addedPersonalDiscount = await _discountService.AddPersonalDiscount(addPersonal);

            return addedPersonalDiscount;
        }     
        
        // POST api/<DiscountController>
        [HttpPost("inventory_item")]
        public async Task<InventoryItemDiscountDTO> AddInventoryItemDiscount([FromBody] AddInventoryItemDiscountDTO addInventory)
        {
            InventoryItemDiscountDTO addedInventoryDiscount = await _discountService.AddInventoryItemDiscount(addInventory);

            return addedInventoryDiscount;

        }

    }
}
