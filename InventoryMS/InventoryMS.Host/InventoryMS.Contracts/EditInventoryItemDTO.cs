using System.Reflection;

namespace InventoryMS.Contracts
{
    public class EditInventoryItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public bool IsUpdated()
        {
            foreach (PropertyInfo property in GetProperties())
                if (property.GetValue(this, null) != null)
                    return true;

            return false;
        }

        public List<PropertyInfo> GetProperties()
        {
            return this.GetType().GetProperties().Where(p => p.Name != "Id").ToList();
        }

    }
}