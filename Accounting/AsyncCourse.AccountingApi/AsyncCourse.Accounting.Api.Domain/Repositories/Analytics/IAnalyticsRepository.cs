using AsyncCourse.Accounting.Api.Models.Analytics;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Analytics;

public interface IAnalyticsRepository
{
    Task UpdateMaxPriceAsync(MaxPriceIssue maxPriceIssue);

    Task<List<MaxPriceIssue>> GetMaxPricesForPeriodAsync(DateTime from, DateTime to);
}