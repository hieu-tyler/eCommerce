using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

// TODO: Move to Infrastructure project if needed
namespace Data.EFContext
{
    public class ECommerceDbContextFactory : IDesignTimeDbContextFactory<ECommerceDbContext>
    {
        public ECommerceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ECommerceDb");
            
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>();
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("BackendApi"));

            return new ECommerceDbContext(optionsBuilder.Options);
        }
            
    }
}
