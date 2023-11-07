using InventoryMS.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
        [Required]
        public int Stock { get; set; }

        public InventoryItem UpdateFromEditInventoryItemDto(EditInventoryItemDTO inventoryItemToEditDTO)
        {
            List<PropertyInfo> properties = inventoryItemToEditDTO.GetProperties();

            if(properties.Count > 0)
            {
                Type typeOfinventoryItem = this.GetType();
                Type typeOfinventoryItemDTO = inventoryItemToEditDTO.GetType();

                foreach (PropertyInfo property in properties)
                {
                    object valueToCopy = typeOfinventoryItemDTO.GetProperty(property.Name).GetValue(inventoryItemToEditDTO, null);

                    PropertyInfo? propertyToUpdate = typeOfinventoryItem.GetProperty(property.Name);

                    if(propertyToUpdate != null)
                    {
                        propertyToUpdate.SetValue(this, valueToCopy, null);

                    }
                }
            }

            return this;
        }
    }
}
