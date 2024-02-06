using AsyncCourse.Core.WarmUp;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Core.Db.DbContextSupport;

public abstract class DbWarmUpBase<TDbContext> : IWarmUp 
    where TDbContext : DbContext
{
    [NotNull] private readonly IDbContextFactory<TDbContext> dbContextFactory;
    [NotNull] private readonly IDbSettings settings;

    public DbWarmUpBase(
        [NotNull] IDbContextFactory<TDbContext> dbContextFactory, 
        [NotNull] IDbSettings settings)
    {
        this.dbContextFactory = dbContextFactory;
        this.settings = settings;
    }

    public async Task RunAsync()
    {
        await using var context = dbContextFactory.CreateDbContext();
        if (settings.DisableMigrations)
        {
            await context.Database.EnsureCreatedAsync();
        }
        else
        {
            await context.Database.MigrateAsync();
        }
    }
}