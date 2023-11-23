using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class SaleDiscount
    {
        [Key]
        public long SaleDiscountId { get; set; }
        [Required]
        public SaleType SaleType { get; set; }
    }
}
