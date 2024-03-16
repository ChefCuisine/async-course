using AsyncCourse.Accounting.Api.Client;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace AsyncCourse.Accounting.AccountsDaemon;

public class AccountsHanler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IAccountingApiClient accountingApiClient;
    private readonly ILog log;

    public AccountsHanler()
    {
        log = new ConsoleLog().WithMinimumLevel(LogLevel.Info);
        kafkaBus = new TemlateKafkaMessageBus();
        accountingApiClient = new AccountingApiClient(AccountingApiLocalAddress.Get(), log);
    }

    public async Task ProcessStreamEvent(CancellationToken cancellationToken)
    {
        var streamResult = ConsumeEvent<MessageBusAccountStreamEvent>(Constants.AccountsStreamTopic, cancellationToken);
        if (streamResult == null)
        {
            log.Error("..."); // todo add log
            return;
        }

        var metaInfo = streamResult.MetaInfo;
        switch (AccountMapper.GetStreamType(metaInfo.EventType))
        {
            case MessageBusStreamEventType.Created:
                var account = AccountMapper.MapAccount(streamResult.Context);
                await accountingApiClient.SaveAccountAsync(account);
                break;
            case MessageBusStreamEventType.Updated:
                // todo
                break;
            case MessageBusStreamEventType.Deleted:
                // todo
                break;
            case MessageBusStreamEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task ProcessBusinessEvent(CancellationToken cancellationToken)
    {
        var businessResult = ConsumeEvent<MessageBusAccountsEvent>(Constants.AccountsTopic, cancellationToken);
        if (businessResult == null)
        {
            log.Error("..."); // todo add log
            return;
        }

        var metaInfo = businessResult.MetaInfo;
        switch (AccountMapper.GetBusinessType(metaInfo.EventType))
        {
            case MessageBusAccountsEventType.RoleChanged:
                var account = AccountMapper.MapAccount(businessResult.Context);
                await accountingApiClient.UpdateAccountAsync(account);
                break;
            case MessageBusAccountsEventType.Unknown:
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