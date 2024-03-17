using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface IGetManagementBalanceCommand
{
    Task<ManagementBalanceInfo> GetBalanceAsync(Guid accountId, DateTime? dateTime = null, int? statDays = null);
} 

public class GetManagementBalanceCommand : IGetManagementBalanceCommand
{
    private readonly IAccountRepository accountRepository;
    private readonly IAccountBalanceRepository accountBalanceRepository;

    public GetManagementBalanceCommand(
        IAccountRepository accountRepository,
        IAccountBalanceRepository accountBalanceRepository)
    {
        this.accountRepository = accountRepository;
        this.accountBalanceRepository = accountBalanceRepository;
    }

    public async Task<ManagementBalanceInfo> GetBalanceAsync(Guid accountId, DateTime? dateTime = null, int? statDays = null)
    {
        // todo расширить ответ, вернуть какой-нибудь вменяемый Error/Enum там где null
        
        var account = await accountRepository.GetAsync(accountId);
        if (account == null)
        {
            return null;
        }

        // todo потом сделаем красиво через Authorize какой-нибудь
        if (account.Role != AccountingAccountRole.Manager || account.Role != AccountingAccountRole.Accountant)
        {
            return null;
        }
        
        if (!dateTime.HasValue)
        {
            dateTime = DateTime.Now;
        }

        statDays ??= 0;

        var allAccountsResult = await accountRepository.GetAccountsAsync();
        var employees = allAccountsResult.Where(x => x.Role == AccountingAccountRole.Employee);
        var dateFrom = dateTime.Value.AddDays(-statDays.Value); // validate datefrom < datetime, possible earlier

        var balancesByDate = new Dictionary<DateTime, decimal>();

        foreach (var employee in employees)
        {
            var balances = await accountBalanceRepository.GetForPeriodAsync(employee.AccountId, dateFrom, dateTime.Value);
            var employeeBalancesByDate = balances.ToDictionary(k => k.Date);

            foreach (var (date, employeeBalance) in employeeBalancesByDate)
            {
                if (!employeeBalance.Total.HasValue)
                {
                    continue;
                }
                if (balancesByDate.ContainsKey(date))
                {
                    balancesByDate[date] += employeeBalance.Total.Value;
                }
                else
                {
                    balancesByDate.Add(date, employeeBalance.Total.Value);
                }
            }
        }

        var responseByDays = balancesByDate.Select(balanceByDate =>
            new ManagementBalanceDayInfo
            {
                Date = balanceByDate.Key,
                Total = balanceByDate.Value
            }).ToList();


        return new ManagementBalanceInfo
        {
            Total = responseByDays.Sum(x => x.Total),
            Days = responseByDays
        };
    }
}