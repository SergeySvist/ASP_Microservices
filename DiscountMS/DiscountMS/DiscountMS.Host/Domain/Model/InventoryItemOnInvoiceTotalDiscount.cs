using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class InventoryItemOnInvoiceTotalDiscount
    {
        [Key]
        public long InventoryItemOnInvoiceTotalDiscountId { get; set; }

        [Required]
        public long InventoryId { get; set; }

        [Required]
        public decimal InvoiceTotal { get; set; }

        [Required]
        public decimal InventoryItemPrice { get; set; }
    }
}
