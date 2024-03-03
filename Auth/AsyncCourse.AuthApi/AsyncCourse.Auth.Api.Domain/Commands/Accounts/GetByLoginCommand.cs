using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IGetByLoginCommand
{
    Task<AuthAccount> GetByLoginAsync(string email, string password);
}

public class GetByLoginCommand : IGetByLoginCommand
{
    private readonly IAccountRepository accountRepository;

    public GetByLoginCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    
    public async Task<AuthAccount> GetByLoginAsync(string email, string password)
    {
        var result = await accountRepository.GetByLoginAndPasswordAsync(email, password);

        if (result != null)
        {
            return result;
        }

        return null;
    }
}