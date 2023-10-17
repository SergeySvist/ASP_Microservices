using Microsoft.EntityFrameworkCore;
using UsersMS.Domain.Models;

namespace UsersMS.Domain.DbCtx
{
    public class UsersMsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UsersMsDb"));
        }

        public DbSet<User> Users { get; set; }
    }
}
