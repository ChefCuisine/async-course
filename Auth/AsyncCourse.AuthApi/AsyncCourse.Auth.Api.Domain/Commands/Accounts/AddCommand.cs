using AsyncCourse.Auth.Api.Domain.Commands.Accounts.Extensions;
using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IAddCommand
{
    Task AddAsync(AuthAccount account);
}

public class AddCommand : IAddCommand
{
    private readonly IAccountRepository accountRepository;
    private readonly ITemlateKafkaMessageBus messageBus;
    
    public AddCommand(IAccountRepository accountRepository, ITemlateKafkaMessageBus messageBus)
    {
        this.accountRepository = accountRepository;
        this.messageBus = messageBus;
    }
    
    public async Task AddAsync(AuthAccount account)
    {
        await accountRepository.AddAsync(account);

        await SendEvents(account);
    }

    private async Task SendEvents(AuthAccount account)
    {
        var streamEventMessage = account
            .GetEventCreated()
            .ToStreamMessage();

        await messageBus.SendMessageAsync(Constants.AccountsStreamTopic, streamEventMessage);
    }
}