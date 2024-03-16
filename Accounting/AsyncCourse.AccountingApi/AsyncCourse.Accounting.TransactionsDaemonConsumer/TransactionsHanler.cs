using AsyncCourse.Accounting.Api.Client;
using AsyncCourse.Accounting.Api.Models.Transactions;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Transactions;
using AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace AsyncCourse.Accounting.TransactionsDaemonConsumer;

public class TransactionsHanler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IAccountingApiClient accountingApiClient;
    private readonly ILog log;

    private readonly Dictionary<DateTime, decimal> maxPricePerDay = new();

    public TransactionsHanler()
    {
        log = new ConsoleLog().WithMinimumLevel(LogLevel.Info);
        kafkaBus = new TemlateKafkaMessageBus();
        accountingApiClient = new AccountingApiClient(AccountingApiLocalAddress.Get(), log);
    }
    
    public async Task ProcessStreamEvent(CancellationToken cancellationToken)
    {
        var streamResult = ConsumeEvent<MessageBusTransactionsStreamEvent>(Constants.TransactionsStreamTopic, cancellationToken);
        if (streamResult == null)
        {
            log.Error("..."); // todo add log
            return;
        }

        var metaInfo = streamResult.MetaInfo;
        switch (TransactionMapper.GetStreamType(metaInfo.EventType))
        {
            case MessageBusStreamEventType.Created:
                var transaction = TransactionMapper.MapTransaction(streamResult.Context);
                await accountingApiClient.UpdateBalanceAsync(transaction.Id);
                break;
            // с транзакцией больше ничего делать нельзя
            case MessageBusStreamEventType.Updated:
            case MessageBusStreamEventType.Deleted:
            case MessageBusStreamEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task ProcessBusinessEvent(CancellationToken cancellationToken)
    {
        var businessResult = ConsumeEvent<MessageBusTransactionsEvent>(Constants.TransactionsTopic, cancellationToken);
        if (businessResult == null)
        {
            log.Error("..."); // todo add log
            return;
        }

        var metaInfo = businessResult.MetaInfo;
        var transaction = TransactionMapper.MapTransaction(businessResult.Context);

        switch (TransactionMapper.GetBusinessType(metaInfo.EventType))
        {
            case MessageBusTransactionEventType.AnalyticsSent:
                if (MaxPriceChanged(transaction))
                {
                    await accountingApiClient.UpdateAnalyticsAsync(transaction.Id);
                }
                break;
            case MessageBusTransactionEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private TEvent ConsumeEvent<TEvent>(string topic, CancellationToken cancellationToken) where TEvent : MessageBusEvent
    {
        var streamResult = kafkaBus.Consume<TEvent>(topic, cancellationToken);
        if (streamResult == default)
        {
            return null;
        }

        var metaInfo = streamResult.MetaInfo;
        if (metaInfo == default)
        {
            return null;
        }

        return streamResult;
    }

    private bool MaxPriceChanged(Transaction transaction)
    {
        if (transaction.Type != TransactionType.IssueDone)
        {
            return false;
        }

        if (maxPricePerDay.TryGetValue(transaction.CreatedAt, out var maxPrice))
        {
            if (maxPrice < transaction.Amount)
            {
                maxPricePerDay[transaction.CreatedAt] = transaction.Amount.Value;
                return true;
            }

            return false;
        }

        maxPricePerDay.Add(transaction.CreatedAt, transaction.Amount.Value);
            
        if (maxPricePerDay.Count > 5)
        {
            var maxKey = maxPricePerDay.Keys.Max();
            var maxValue = maxPricePerDay[maxKey];
            maxPricePerDay.Clear();
            maxPricePerDay.Add(maxKey, maxValue);
        }

        return true;
    }
}