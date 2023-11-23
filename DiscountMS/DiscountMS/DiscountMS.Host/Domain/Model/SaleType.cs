using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class SaleType
    {
        [Key]
        public int SaleTypeId { get; set; }
        [Required]
        public string SaleTypeName { get; set; }

    }
}
