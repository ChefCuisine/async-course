using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Core.Db;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Accounting.Api.Db;

public class AccountingApiDbContext : DbContext
{
    [NotNull] private readonly IDbSettings settings;
    [NotNull] private readonly ILoggerFactory loggerFactory;

    public AccountingApiDbContext(
        [NotNull] IDbSettings settings,
        [NotNull] ILoggerFactory loggerFactory)
    {
        this.settings = settings;
        this.loggerFactory = loggerFactory;
    }

    [NotNull] public DbSet<AccountingAccountDbo> Accounts { get; set; }
    [NotNull] public DbSet<AccountingIssueDbo> Issues { get; set; }
    [NotNull] public DbSet<TransactionDbo> Transactions { get; set; }
    [NotNull] public DbSet<TransactionOutboxEventDbo> TransactionEvents { get; set; }
    [NotNull] public DbSet<AccountBalanceDbo> AccountBalances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(settings.ConnectionString);
        builder.UseLoggerFactory(loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var transactionDbos = modelBuilder.Entity<TransactionDbo>();
        transactionDbos.HasKey(x => x.Id);
        
        var accountDbos = modelBuilder.Entity<AccountingAccountDbo>();
        accountDbos.HasKey(x => x.AccountId);
        
        var issueDbos = modelBuilder.Entity<AccountingIssueDbo>();
        issueDbos.HasKey(x => x.IssueId);
        
        var transactionEventsDbos = modelBuilder.Entity<TransactionOutboxEventDbo>();
        transactionEventsDbos.HasKey(x => x.Id);
        
        var accountBalanceDbos = modelBuilder.Entity<AccountBalanceDbo>();
        accountBalanceDbos.HasKey(x => x.AccountId);
    }
}