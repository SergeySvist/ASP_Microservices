using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class Discount
    {
        [Key]
        public long DiscountId { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        [Required]
        public DiscountAmountType DiscountAmountType { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }
        [Required]
        public DateTime DateFrom { get; set;}

        public DateTime DateTo { get; set;}
        [Required]
        public DiscountTerminationType DiscountTerminationType { get; set; }
        [Required]
        public long SpecificDiscountTableKey { get; set; }
    }
}
