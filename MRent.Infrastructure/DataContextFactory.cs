using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure
{
    public class DataContextFactory : IDesignTimeDbContextFactory<PostgresContext>
    {
        public PostgresContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=Database;User Id=postgres;Password=12345678;");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return new PostgresContext(optionsBuilder.Options);
        }
    }
}
