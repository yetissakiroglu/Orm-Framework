using Code.OrmFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Code.OrmFramework.Database
{
    public class PostgreSqlProvider : IDatabaseProvider
    {
        public DbContextOptions<AppDbContext> GetDbContextOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder.Options;
        }
    }
}
