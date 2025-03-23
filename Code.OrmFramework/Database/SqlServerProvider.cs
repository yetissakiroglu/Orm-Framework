using Code.OrmFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Code.OrmFramework.Database
{
    public class SqlServerProvider : IDatabaseProvider
    {
        public DbContextOptions<AppDbContext> GetDbContextOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }
    }
}
