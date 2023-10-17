using System.ComponentModel.DataAnnotations;

namespace InventoryMS.Host.Domain.Models
{
    public class InventoryItem
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

    }
}
