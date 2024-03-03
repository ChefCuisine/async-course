using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IEditCommand
{
    Task EditAsync(EditAuthAccount account);
}

public class EditCommand : IEditCommand // todo вероятно прямо отсюда можно будет посылать события AccountRoleChanged
{
    private readonly IAccountRepository accountRepository;

    public EditCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    
    public async Task EditAsync(EditAuthAccount account)
    {
        await accountRepository.EditAsync(account);
    }
}