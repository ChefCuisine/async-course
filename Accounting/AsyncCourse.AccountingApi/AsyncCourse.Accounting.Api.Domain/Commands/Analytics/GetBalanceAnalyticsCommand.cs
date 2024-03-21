using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Analytics;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Analytics;

public interface IGetBalanceAnalyticsCommand
{
    Task<EarnedTodayBalance> GetAsync();
}

public class GetBalanceAnalyticsCommand : IGetBalanceAnalyticsCommand
{
    private readonly IAccountRepository accountRepository;
    private readonly IAccountBalanceRepository accountBalanceRepository;

    public GetBalanceAnalyticsCommand(
        IAccountRepository accountRepository,
        IAccountBalanceRepository accountBalanceRepository)
    {
        this.accountRepository = accountRepository;
        this.accountBalanceRepository = accountBalanceRepository;
    }

    public async Task<EarnedTodayBalance> GetAsync()
    {
        var dateTime = DateTime.Now;

        var allAccountsResult = await accountRepository.GetAccountsAsync();
        var employees = allAccountsResult.Where(x => x.Role == AccountingAccountRole.Employee);

        int negativeBalanceCounter = 0;
        decimal todayEarned = 0;
        
        foreach (var employee in employees)
        {
            var balance = await accountBalanceRepository.GetAsync(employee.AccountId, dateTime);
            if (!balance.Total.HasValue)
            {
                continue;
            }

            if (balance.Total < 0)
            {
                negativeBalanceCounter += 1;
            }

            todayEarned += balance.Total.Value;
        }
        
        return new EarnedTodayBalance
        {
            PopugsWithNegativeBalance = negativeBalanceCounter,
            ManagementEarnedToday = todayEarned
        };
    }
}