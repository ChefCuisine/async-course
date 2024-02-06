using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Core.Db.DbContextSupport;

public interface IDbContextCreator<out TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    TDbContext Create([NotNull] IDbSettings settings, [NotNull] ILoggerFactory loggerFactory);
}