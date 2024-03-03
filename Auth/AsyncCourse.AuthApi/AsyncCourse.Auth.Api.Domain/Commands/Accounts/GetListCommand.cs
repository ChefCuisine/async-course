using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts;

public interface IGetListCommand
{
    Task<List<AuthAccount>> GetListAsync();
}

public class GetListCommand : IGetListCommand
{
    private readonly IAccountRepository accountRepository;

    public GetListCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    
    public async Task<List<AuthAccount>> GetListAsync()
    {
        var result = await accountRepository.GetListAsync();

        return result;
    }
}