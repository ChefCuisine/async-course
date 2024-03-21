using AsyncCourse.Accounting.Api.Client;
using AsyncCourse.Accounting.Api.Models.Balances;
using AsyncCourse.Accounting.CronDaemonProducer.Extensions;
using AsyncCourse.Template.Kafka.MessageBus;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace AsyncCourse.Accounting.CronDaemonProducer;

public class CronJobHandler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IAccountingApiClient accountingApiClient;
    private readonly ILog log;

    public CronJobHandler()
    {
        log = new ConsoleLog().WithMinimumLevel(LogLevel.Info);
        kafkaBus = new TemlateKafkaMessageBus();
        accountingApiClient = new AccountingApiClient(AccountingApiLocalAddress.Get(), log);
    }
    
    public async Task DoDailyWork()
    {
        // В конце дня необходимо:
        // a) считать сколько денег сотрудник получил за рабочий день
        // b) отправлять на почту сумму выплаты
        
        // Поэтому мы на каждый AccountBalance создаем по события:
        // стрим-событие - для переноса/обнуления баланса
        // бизнес-событие - для создания результирующей транзакции, которая в свою очередь создаст событие на отправку письма

        var balancesResult = await accountingApiClient.GetAllForDateAsync();
        if (!balancesResult.IsSuccessful)
        {
            log.Error($"Couldn't recieve account balances: {balancesResult.ResponseCode}");
        }

        foreach (var balance in balancesResult.Result)
        {
            await SendEventsAsync(balance);
        }
    }

    private async Task SendEventsAsync(AccountBalance accountBalance)
    {
        var streamEventMessage = accountBalance
            .GetEventCreated()
            .ToStreamMessage();
        
        var businessMessage = accountBalance
            .GetEventNewDayBalanceCreated()
            .ToBusinessMessage();
        
        await kafkaBus.SendMessageAsync(Constants.AccountBalanceStreamTopic, streamEventMessage);
        await kafkaBus.SendMessageAsync(Constants.AccountBalanceTopic, businessMessage);
    }
}