using AsyncCourse.Accounting.Api.Client;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.AccountBalances;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.AccountBalances;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace AsyncCourse.Accounting.CronDaemonConsumer;

public class AccountBalanceHanler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IAccountingApiClient accountingApiClient;
    private readonly ILog log;
    
    public AccountBalanceHanler()
    {
        log = new ConsoleLog().WithMinimumLevel(LogLevel.Info);
        kafkaBus = new TemlateKafkaMessageBus();
        accountingApiClient = new AccountingApiClient(AccountingApiLocalAddress.Get(), log);
    }
    
    public async Task ProcessStreamEvent(CancellationToken cancellationToken)
    {
        var streamResult = ConsumeEvent<MessageBusAccountBalanceStreamEvent>(Constants.AccountBalanceStreamTopic, cancellationToken);
        if (streamResult == null)
        {
            log.Error("..."); // todo add log
            return;
        }

        var metaInfo = streamResult.MetaInfo;
        var accountBalance = AccountBalanceMapper.MapBalance(streamResult.Context);

        switch (AccountBalanceMapper.GetStreamType(metaInfo.EventType))
        {
            case MessageBusStreamEventType.Created:
                // баланс обнуляется или переносится на следующий день
                await accountingApiClient.RenewBalanceAsync(accountBalance.AccountId, accountBalance.Date);
                break;
            case MessageBusStreamEventType.Updated:
            case MessageBusStreamEventType.Deleted:
            case MessageBusStreamEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task ProcessBusinessEvent(CancellationToken cancellationToken)
    {
        var businessResult = ConsumeEvent<MessageBusAccountBalanceEvent>(Constants.AccountBalanceTopic, cancellationToken);
        if (businessResult == null)
        {
            log.Error("..."); // todo add log
            return;
        }

        var metaInfo = businessResult.MetaInfo;
        var accountBalance = AccountBalanceMapper.MapBalance(businessResult.Context);

        switch (AccountBalanceMapper.GetBusinessType(metaInfo.EventType))
        {
            case MessageBusAccountBalanceEventType.NextDayBalanceCreated:
                // создается результирующая транзакция
                await accountingApiClient.CreateDayResultTransactionAsync(accountBalance.AccountId, accountBalance.Date);
                break;
            case MessageBusAccountBalanceEventType.Unknown:
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