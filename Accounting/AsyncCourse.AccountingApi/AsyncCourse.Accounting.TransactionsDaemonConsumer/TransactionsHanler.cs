using AsyncCourse.Accounting.Api.Client;
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
        switch (TransactionMapper.GetBusinessType(metaInfo.EventType))
        {
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
}