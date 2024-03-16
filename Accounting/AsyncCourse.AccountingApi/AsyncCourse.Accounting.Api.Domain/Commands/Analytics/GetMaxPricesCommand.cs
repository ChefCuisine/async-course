using AsyncCourse.Accounting.Api.Domain.Repositories.Analytics;
using AsyncCourse.Accounting.Api.Models.Analytics;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Analytics;

public interface IGetMaxPricesCommand
{
    Task<List<MaxPriceIssue>> GetAsync(DateTime from, DateTime to);
}

public class GetMaxPricesCommand : IGetMaxPricesCommand
{
    private readonly IAnalyticsRepository analyticsRepository;

    public GetMaxPricesCommand(IAnalyticsRepository analyticsRepository)
    {
        this.analyticsRepository = analyticsRepository;
    }

    public async Task<List<MaxPriceIssue>> GetAsync(DateTime from, DateTime to)
    {
        var result = await analyticsRepository.GetMaxPricesForPeriodAsync(from, to);

        return result;
    }
}