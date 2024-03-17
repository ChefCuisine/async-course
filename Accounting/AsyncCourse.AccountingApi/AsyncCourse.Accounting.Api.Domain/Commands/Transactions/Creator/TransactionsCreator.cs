using AsyncCourse.Accounting.Api.Domain.Commands.Issues.Calculator;
using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Provider;
using AsyncCourse.Accounting.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Creator;

public interface ITransactionsCreator
{
    Task CreateAsync(TransactionType transactionType, Guid issueId, Guid? assignedToAccountId);
    Task CreateDayResultAsync(Guid accountId, DateTime date);
}

public class TransactionsCreator : ITransactionsCreator
{
    private readonly IIssueCalculator issueCalculator;
    private readonly ITransactionRepository transactionRepository;
    private readonly ITransactionOutboxEventRepository transactionOutboxEventRepository;
    private readonly ITransactionInfoProvider transactionsProvider;

    public TransactionsCreator(
        IIssueCalculator issueCalculator,
        ITransactionRepository transactionRepository,
        ITransactionOutboxEventRepository transactionOutboxEventRepository,
        ITransactionInfoProvider transactionsProvider)
    {
        this.issueCalculator = issueCalculator;
        this.transactionRepository = transactionRepository;
        this.transactionOutboxEventRepository = transactionOutboxEventRepository;
        this.transactionsProvider = transactionsProvider;
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
            Amount = price.Value
        };
        
        await transactionRepository.AddAsync(transaction);
        
        var transactionEvent = new TransactionOutboxEvent
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Type = MapType(transactionType),
            TransactionId = transaction.Id,
            Amount = price.Value,
        };
        await transactionOutboxEventRepository.AddAsync(transactionEvent);
    }

    public async Task CreateDayResultAsync(Guid accountId, DateTime date)
    {
        var accountTransactions = await transactionsProvider.GetTransactionBalanceInfosAsync(accountId, date);
        var totalPerDay = accountTransactions.Sum(x => x.Amount);
        
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Type = TransactionType.DayClosed,
            Amount = totalPerDay,
            ClosedDayInfo = new ClosedDayInfo
            {
                AccountId = accountId,
                Transactions = accountTransactions
            }
        };
        
        await transactionRepository.AddAsync(transaction);
        
        var transactionEvent = new TransactionOutboxEvent
        {
            Id = Guid.NewGuid(),
            CreatedAt = transaction.CreatedAt,
            Type = MapType(transaction.Type),
            TransactionId = transaction.Id,
            Amount = transaction.Amount ?? 0,
        };
        await transactionOutboxEventRepository.AddAsync(transactionEvent);
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

    private static TransactionOutboxEventType MapType(TransactionType transactionType)
    {
        switch (transactionType)
        {
            case TransactionType.IssueAssigned:
                return TransactionOutboxEventType.RemoveMoney;
            case TransactionType.IssueDone:
                return TransactionOutboxEventType.AddMoney;
            case TransactionType.DayClosed:
                return TransactionOutboxEventType.DayClosed;
            case TransactionType.Unknown:
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
        }
    }
}