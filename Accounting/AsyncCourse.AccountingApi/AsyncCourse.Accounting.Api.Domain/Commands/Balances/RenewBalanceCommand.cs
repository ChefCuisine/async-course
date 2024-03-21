using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface IRenewBalanceCommand
{
    Task RenewBalanceAsync(Guid accountId, DateTime dateTime);
}

public class RenewBalanceCommand : IRenewBalanceCommand
{
    private readonly IAccountBalanceRepository accountBalanceRepository;

    public RenewBalanceCommand(IAccountBalanceRepository accountBalanceRepository)
    {
        this.accountBalanceRepository = accountBalanceRepository;
    }

    public async Task RenewBalanceAsync(Guid accountId, DateTime dateTime)
    {
        var balanceForLastDay = await accountBalanceRepository.GetAsync(accountId, dateTime);
        
        var newBalance = new AccountBalance
        {
            Id = Guid.NewGuid(),
            AccountId = accountId,
            Date = dateTime.AddDays(1)
        };
        
        if (!balanceForLastDay.Total.HasValue || balanceForLastDay.Total > 0)
        {
            newBalance.Total = 0;
        }
        else
        {
            newBalance.Total = balanceForLastDay.Total;
        }

        await accountBalanceRepository.CreateAsync(newBalance);
    }
}