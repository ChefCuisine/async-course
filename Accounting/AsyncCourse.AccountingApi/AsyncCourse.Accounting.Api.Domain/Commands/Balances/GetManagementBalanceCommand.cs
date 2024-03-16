using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface IGetManagementBalanceCommand
{
    Task<ManagementBalanceInfo> GetBalanceAsync(Guid accountId, DateTime? dateTime = null, int? statDays = 0);
} 

public class GetManagementBalanceCommand : IGetManagementBalanceCommand
{
    private readonly IAccountRepository accountRepository;

    public GetManagementBalanceCommand(
        IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task<ManagementBalanceInfo> GetBalanceAsync(Guid accountId, DateTime? dateTime = null, int? statDays = 0)
    {
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
        
        return new ManagementBalanceInfo();
    }
}