using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class DiscountTerminationType
    {
        [Key]
        public int DiscountTerminationTypeId { get; set; }
        [Required]
        public string DiscountTerminationTypeName { get; set; }

    }
}
