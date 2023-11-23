using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class DiscountAmountType
    {
        [Key]
        public int DiscountAmountTypeId { get; set; }
        [Required]
        public string DiscountAmontTypeName { get; set; }

    }
}
