using AsyncCourse.Auth.Api.Db.Dbos;
using AsyncCourse.Core.Db;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Auth.Api.Db;

public class AuthApiDbContext : DbContext
{
    [NotNull] private readonly IDbSettings settings;
    [NotNull] private readonly ILoggerFactory loggerFactory;

    public AuthApiDbContext(
        [NotNull] IDbSettings settings,
        [NotNull] ILoggerFactory loggerFactory)
    {
        this.settings = settings;
        this.loggerFactory = loggerFactory;
    }

    [NotNull] public DbSet<AuthAccountDbo> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(settings.ConnectionString);
        builder.UseLoggerFactory(loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var accountDbos = modelBuilder.Entity<AuthAccountDbo>();
        accountDbos.HasKey(x => x.Id);
    }
}