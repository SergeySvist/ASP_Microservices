using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class InventoryItemDiscount
    {
        [Key]
        public long InventoryItemDiscountId { get; set; }
        [Required]
        public long InventoryItemId { get; set; }

    }
}
