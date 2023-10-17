using InventoryMS.Host.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Host.Domain.DbCtx
{
    public class InventoryMsDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("InventoryMsDb"));
        }

        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}
