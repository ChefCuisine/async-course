using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using Newtonsoft.Json;

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

        var kafkaAccount = Map(account);
        var message = JsonConvert.SerializeObject(kafkaAccount);

        await messageBus.SendMessageAsync(Constants.AccountCreateTopic, message);
    }

    private static MessageBusAccount Map(AuthAccount account)
    {
        return new MessageBusAccount
        {
            Id = account.Id,
            Email = account.Email,
            Name = account.Email,
            Surname = account.Surname,
            Role = account.Role.ToString()
        };
    }
}