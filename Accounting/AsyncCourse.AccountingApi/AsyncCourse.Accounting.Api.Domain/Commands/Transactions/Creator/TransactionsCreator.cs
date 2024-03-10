using AsyncCourse.Accounting.Api.Domain.Commands.Issues.Calculator;
using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Extensions;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.Transactions;
using AsyncCourse.Template.Kafka.MessageBus;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Creator;

public interface ITransactionsCreator
{
    Task CreateAsync(TransactionType transactionType, Guid issueId, Guid? assignedToAccountId);
}

public class TransactionsCreator : ITransactionsCreator
{
    private readonly IIssueCalculator issueCalculator;
    private readonly ITransactionRepository transactionRepository;
    private readonly ITemlateKafkaMessageBus messageBus;

    public TransactionsCreator(
        IIssueCalculator issueCalculator,
        ITransactionRepository transactionRepository,
        ITemlateKafkaMessageBus messageBus)
    {
        this.issueCalculator = issueCalculator;
        this.transactionRepository = transactionRepository;
        this.messageBus = messageBus;
    }

    public async Task CreateAsync(TransactionType transactionType, Guid issueId, Guid? assignedToAccountId)
    {
        var price = GetPrice(transactionType);

        if (!assignedToAccountId.HasValue || !price.HasValue)
        {
            return;
        }
        
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Type = transactionType,
            IssueInfo = new IssueTransactionInfo
            {
                IssueId = issueId,
                AssignToAccountId = assignedToAccountId.Value
            },
            Amount = price
        };
        
        await transactionRepository.AddAsync(transaction);

        await SendEventsAsync(transaction);
    }

    private decimal? GetPrice(TransactionType transactionType)
    {
        if (transactionType == TransactionType.IssueAssigned)
        {
            return issueCalculator.GetPriceToAssign();
        }

        if (transactionType == TransactionType.IssueDone)
        {
            return issueCalculator.GetPriceToDone();
        }

        return null;
    }
    
    private async Task SendEventsAsync(Transaction transaction)
    {
        var streamEventMessage = transaction
            .GetEventCreated()
            .ToStreamMessage();

        await messageBus.SendMessageAsync(Constants.TransactionsStreamTopic, streamEventMessage);
    }
}