using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface IGetAllBalanceCommand
{
    Task<List<AccountBalance>> GetAsync(DateTime? dateTime = null);
}

public class GetAllBalanceCommand : IGetAllBalanceCommand
{
    private readonly IAccountBalanceRepository accountBalanceRepository;

    public GetAllBalanceCommand(IAccountBalanceRepository accountBalanceRepository)
    {
        this.accountBalanceRepository = accountBalanceRepository;
    }

    public async Task<List<AccountBalance>> GetAsync(DateTime? dateTime = null)
    {
        if (!dateTime.HasValue)
        {
            dateTime = DateTime.Now;
        }
        
        var balances = await accountBalanceRepository.GetAllAsync(dateTime.Value);

        return balances;
    }
}