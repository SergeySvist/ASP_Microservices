using System.Reflection;

namespace InventoryMS.Contracts
{
    public class EditInventoryItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }


        public bool IsUpdated()
        {
            return GetProperties().Count > 0;
        }

        public List<PropertyInfo> GetProperties()
        {
            List<PropertyInfo> valueProperties = this.GetType().GetProperties().Where(p => p.Name != "Id").ToList();
            List<PropertyInfo> propertiesForUpdate = new List<PropertyInfo>();

            if (valueProperties.Count > 0 )
            {
                foreach (PropertyInfo property in valueProperties)
                {
                    if(property.GetValue(this, null) != null)
                    {
                        propertiesForUpdate.Add(property);
                    }
                }
            }

            return propertiesForUpdate;
        }

    }
}