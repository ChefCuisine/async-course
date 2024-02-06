using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Core.Db.DbContextSupport;

public class DbContextFactory<TDbContext> : IDbContextFactory<TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    [NotNull] private readonly IDbSettings settings;
    [NotNull] private readonly IDbContextCreator<TDbContext> dbContextCreator;
    [NotNull] private readonly ILoggerFactory loggerFactory;

    public DbContextFactory(
        [NotNull] IDbSettings settings,
        [NotNull] IDbContextCreator<TDbContext> dbContextCreator)
    {
        this.settings = settings;
        this.dbContextCreator = dbContextCreator;
        loggerFactory = new LoggerFactory();
    }

    [NotNull]
    public TDbContext CreateDbContext() => dbContextCreator.Create(settings, loggerFactory);
}