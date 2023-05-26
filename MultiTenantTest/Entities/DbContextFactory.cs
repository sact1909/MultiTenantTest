using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantTest.Entities
{
    public class DbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new TestDbContext(builder.Options);
        }
    }
}
