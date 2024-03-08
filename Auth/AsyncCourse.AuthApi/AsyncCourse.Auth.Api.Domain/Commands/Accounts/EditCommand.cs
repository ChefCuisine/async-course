using AsyncCourse.Auth.Api.Domain.Commands.Accounts.Extensions;
using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;

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
        if (updatedAccount == null)
        {
            return;
        }

        await SendEvents(updatedAccount);
    }

    private async Task SendEvents(AuthAccount updatedAccount)
    {
        var businessEventMessage = updatedAccount
            .GetEventRoleChanged()
            .ToBusinessMessage();

        await messageBus.SendMessageAsync(Constants.AccountsTopic, businessEventMessage);
    }
}