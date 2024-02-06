using JetBrains.Annotations;

namespace AsyncCourse.Core.Db.DbContextSupport;

public interface IDbContextFactory<out TDbContext> 
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    [NotNull]
    TDbContext CreateDbContext();
}