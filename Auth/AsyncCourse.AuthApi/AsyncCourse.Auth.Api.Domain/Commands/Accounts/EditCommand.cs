using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using Newtonsoft.Json;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IEditCommand
{
    Task EditAsync(EditAuthAccount account);
}

public class EditCommand : IEditCommand
{
    private readonly IAccountRepository accountRepository;
    private readonly ITemlateKafkaMessageBus messageBus;

    public EditCommand(IAccountRepository accountRepository, ITemlateKafkaMessageBus messageBus)
    {
        this.accountRepository = accountRepository;
        this.messageBus = messageBus;
    }
    
    public async Task EditAsync(EditAuthAccount account)
    {
        var updatedAccount = await accountRepository.EditAsync(account);
        
        var kafkaAccount = Map(updatedAccount);
        var message = JsonConvert.SerializeObject(kafkaAccount);

        await messageBus.SendMessageAsync(Constants.AccountUpdateTopic, message);
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