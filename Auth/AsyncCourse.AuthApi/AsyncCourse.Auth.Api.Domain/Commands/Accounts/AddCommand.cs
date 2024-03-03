using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IAddCommand
{
    Task AddAsync(AuthAccount account);
}

public class AddCommand : IAddCommand // todo вероятно прямо отсюда можно будет посылать события AccountCreated
{
    private readonly IAccountRepository accountRepository;
    
    public AddCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    
    public async Task AddAsync(AuthAccount account)
    {
        await accountRepository.AddAsync(account);
    }
}