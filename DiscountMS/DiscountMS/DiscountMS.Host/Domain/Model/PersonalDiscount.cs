using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{
    public class PersonalDiscount
    {
        [Key]
        public long PersonalDiscountId { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
