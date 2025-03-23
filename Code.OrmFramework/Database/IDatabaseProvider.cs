using Code.OrmFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Code.OrmFramework.Database
{
    public interface IDatabaseProvider
    {
        DbContextOptions<AppDbContext> GetDbContextOptions(string connectionString);
    }
}
