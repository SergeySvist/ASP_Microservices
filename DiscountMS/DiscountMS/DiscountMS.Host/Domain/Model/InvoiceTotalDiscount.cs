using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class InvoiceTotalDiscount
    {
        [Key]
        public long InvoiceTotalDiscountId { get; set; }
        [Required]
        public decimal InvoiceTotal { get; set; }
    }
}
