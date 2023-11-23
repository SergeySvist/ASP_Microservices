using System.ComponentModel.DataAnnotations;

namespace DiscountMS.Host.Domain.Model
{

    //Personal, ForInventoryItem, ByInvoiceTotal etc.
    public class DiscountType
    {
        [Key]
        public int DiscountTypeId { get; set; }
        [Required]
        public string DiscountTypeName { get; set; }

        
    }
}
