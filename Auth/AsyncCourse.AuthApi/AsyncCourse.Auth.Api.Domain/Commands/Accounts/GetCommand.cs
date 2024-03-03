using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IGetCommand
{
    Task<AuthAccount> GetByIdAsync(Guid id);
}

public class GetCommand : IGetCommand
{
    private readonly IAccountRepository accountRepository;

    public GetCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task<AuthAccount> GetByIdAsync(Guid id)
    {
        var account = await accountRepository.GetByIdAsync(id);

        if (account != null)
        {
            return account;
        }

        return null;
    }
}