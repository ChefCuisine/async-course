using AsyncCourse.Accounting.Api.Client;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;
using AsyncCourse.Accounting.DaemonProducer.Extensions;
using AsyncCourse.Template.Kafka.MessageBus;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace AsyncCourse.Accounting.DaemonProducer;

public class TransactionsHandler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IAccountingApiClient accountingApiClient;
    private readonly ILog log;

    public TransactionsHandler()
    {
        log = new ConsoleLog().WithMinimumLevel(LogLevel.Info);
        kafkaBus = new TemlateKafkaMessageBus();
        accountingApiClient = new AccountingApiClient(AccountingApiLocalAddress.Get(), log);
    }
    
    public async Task ProduceEvent()
    {
        var nextEventResult = await accountingApiClient.ReadTransactionEventAsync();
        if (!nextEventResult.IsSuccessful)
        {
            log.Error("...");
            return;
        }

        var nextEvent = nextEventResult.Result;

        try
        {
            await SendEventsAsync(nextEvent);
            await accountingApiClient.DeleteTransactionEventAsync(nextEvent.Id);
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }
    }
    
    private async Task SendEventsAsync(TransactionOutboxEvent transactionEvent)
    {
        if (transactionEvent == null)
        {
            return;
        }
        
        // пока логика одинаковая но скорее всего что-то изменится в будущем
        switch (transactionEvent.Type)
        {
            case TransactionOutboxEventType.RemoveMoney:
            case TransactionOutboxEventType.AddMoney:
            {
                var streamEventMessage = transactionEvent
                    .GetEventCreated()
                    .ToStreamMessage();

                await kafkaBus.SendMessageAsync(Constants.TransactionsStreamTopic, streamEventMessage);
            }
                break;
            case TransactionOutboxEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}