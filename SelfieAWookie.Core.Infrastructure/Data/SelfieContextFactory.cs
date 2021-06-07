using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SelfieAWookie.Core.Infrastructure.Data
{
    public class SelfieContextFactory : IDesignTimeDbContextFactory<SelfieContext>
    {
        #region Public methods

        public SelfieContext CreateDbContext(string[] args)
        {
            var confBuilder = new ConfigurationBuilder();
            confBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings", "appsettings.json"));
            var configurationRoot = confBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<SelfieContext>();
            optionsBuilder.UseSqlServer(
                configurationRoot.GetConnectionString("SelfieDatabase"),
                b => b.MigrationsAssembly("SelfieAWookie.Core.Data.Migration")
            );

            var context = new SelfieContext(optionsBuilder.Options);

            return context;
        }

        #endregion
    }
}