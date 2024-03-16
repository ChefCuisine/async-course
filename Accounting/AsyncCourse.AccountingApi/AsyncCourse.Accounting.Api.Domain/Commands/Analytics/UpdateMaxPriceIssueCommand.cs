using AsyncCourse.Accounting.Api.Domain.Repositories.Analytics;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.Analytics;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Analytics;

public interface IUpdateMaxPriceIssueCommand
{
    Task UpdateAsync(Guid transactionId);
}

public class UpdateMaxPriceIssueCommand : IUpdateMaxPriceIssueCommand
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IAnalyticsRepository analyticsRepository;

    public UpdateMaxPriceIssueCommand(
        ITransactionRepository transactionRepository,
        IAnalyticsRepository analyticsRepository)
    {
        this.transactionRepository = transactionRepository;
        this.analyticsRepository = analyticsRepository;
    }

    public async Task UpdateAsync(Guid transactionId)
    {
        var transaction = await transactionRepository.GetAsync(transactionId);

        var maxPriceIssue = new MaxPriceIssue
        {
            Id = Guid.NewGuid(),
            TransactionId = transactionId,
            IssueId = transaction.IssueInfo.IssueId,
            Amount = transaction.Amount.Value,
            Date = transaction.CreatedAt
        };

        await analyticsRepository.UpdateMaxPriceAsync(maxPriceIssue);
    }
}