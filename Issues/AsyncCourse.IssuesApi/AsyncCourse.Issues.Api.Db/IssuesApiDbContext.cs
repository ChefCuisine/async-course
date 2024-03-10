using AsyncCourse.Core.Db;
using AsyncCourse.Issues.Api.Db.Dbos;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Issues.Api.Db;

public class IssuesApiDbContext : DbContext
{
    [NotNull] private readonly IDbSettings settings;
    [NotNull] private readonly ILoggerFactory loggerFactory;

    public IssuesApiDbContext(
        [NotNull] IDbSettings settings,
        [NotNull] ILoggerFactory loggerFactory)
    {
        this.settings = settings;
        this.loggerFactory = loggerFactory;
    }

    [NotNull] public DbSet<IssueDbo> Issues { get; set; }
    [NotNull] public DbSet<IssueAccountDbo> Accounts { get; set; }
    [NotNull] public DbSet<IssueOutboxEventDbo> IssueEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(settings.ConnectionString);
        builder.UseLoggerFactory(loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var issueDbos = modelBuilder.Entity<IssueDbo>();
        issueDbos.HasKey(x => x.Id);
        
        var accountDbos = modelBuilder.Entity<IssueAccountDbo>();
        accountDbos.HasKey(x => x.AccountId);
        
        var issueEventDbos = modelBuilder.Entity<IssueOutboxEventDbo>();
        issueEventDbos.HasKey(x => x.Id);
    }
}