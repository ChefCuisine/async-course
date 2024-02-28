using AsyncCourse.Core.Db;
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

    // [NotNull] public DbSet<TemplateDomainModelDbo> TemplateDomainModelDbos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(settings.ConnectionString);
        builder.UseLoggerFactory(loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // var templateDomainModelDbos = modelBuilder.Entity<TemplateDomainModelDbo>();
        // templateDomainModelDbos.HasKey(x => x.Id);
    }
}