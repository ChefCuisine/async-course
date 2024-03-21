using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Accounting.Api.Models.Analytics;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Analytics;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;
    
    public AnalyticsRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task UpdateMaxPriceAsync(MaxPriceIssue maxPriceIssue)
    {
        var existingDbo = await accountingApiDbContext.MaxPriceIssues.FindAsync(maxPriceIssue.Date);
        if (existingDbo == null)
        {
            await accountingApiDbContext.MaxPriceIssues.AddAsync(DomainToDbo(maxPriceIssue));
        }
        else
        {
            accountingApiDbContext.MaxPriceIssues.Update(existingDbo);
        }

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task<List<MaxPriceIssue>> GetMaxPricesForPeriodAsync(DateTime from, DateTime to)
    {
        var result = accountingApiDbContext.MaxPriceIssues.Where(x => x.Date >= from && x.Date <= to);
        
        var mappedResult = result.AsEnumerable().Select(DboToDomain).ToList();

        return mappedResult;
    }
    
    #region Mapping

    private static MaxPriceIssueDbo DomainToDbo(MaxPriceIssue maxPriceIssue)
    {
        return new MaxPriceIssueDbo
        {
            Id = maxPriceIssue.Id,
            TransactionId = maxPriceIssue.TransactionId,
            IssueId = maxPriceIssue.IssueId,
            Amount = maxPriceIssue.Amount,
            Date = maxPriceIssue.Date
        };
    }

    private static MaxPriceIssue DboToDomain(MaxPriceIssueDbo dbo)
    {
        return new MaxPriceIssue
        {
            Id = dbo.Id,
            TransactionId = dbo.TransactionId,
            IssueId = dbo.IssueId,
            Amount = dbo.Amount,
            Date = dbo.Date
        };
    }
    
    #endregion
}