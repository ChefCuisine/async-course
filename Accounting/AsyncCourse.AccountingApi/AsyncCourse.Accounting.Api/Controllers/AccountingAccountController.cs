using AsyncCourse.Accounting.Api.Domain.Commands.Accounts;
using AsyncCourse.Accounting.Api.Domain.Commands.Balances;
using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances.Models;
using AsyncCourse.Accounting.Api.Models.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("accounting-account")]
public class AccountingAccountController : Controller
{
    private readonly IAddAccountCommand addAccountCommand;
    private readonly IUpdateAccountCommand updateAccountCommand;
    private readonly IGetBalanceCommand getBalanceCommand;
    private readonly IGetManagementBalanceCommand getManagementBalanceCommand;

    public AccountingAccountController(
        IAddAccountCommand addAccountCommand,
        IUpdateAccountCommand updateAccountCommand,
        IGetBalanceCommand getBalanceCommand,
        IGetManagementBalanceCommand getManagementBalanceCommand)
    {
        this.addAccountCommand = addAccountCommand;
        this.updateAccountCommand = updateAccountCommand;
        this.getBalanceCommand = getBalanceCommand;
        this.getManagementBalanceCommand = getManagementBalanceCommand;
    }
    
    [HttpPost("save")]
    public async Task<bool> Save(AccountingAccount account)
    {
        await addAccountCommand.AddAsync(account);

        return true;
    }
    
    [HttpPost("update")]
    public async Task<bool> Update(AccountingAccount account)
    {
        await updateAccountCommand.UpdateAsync(account);

        return true;
    }
    
    // dashboard methods
    
    [HttpGet("show-by-id")]
    public async Task<AccountBalanceInfoModel> ShowById([FromQuery] Guid accountId, DateTime? dateTime = null)
    {
        // должен показать какой на сегодня баланс
        // + лог операций
        var result = await getBalanceCommand.GetBalanceAsync(accountId, dateTime);
        
        return AccountBalanceMapping.MapFromDomainResult(result);
    }
    
    // authorize только для админов и бухгалтеров
    [HttpGet("show")]
    public async Task<ManagementBalanceInfoModel> Show([FromQuery] Guid accountId, DateTime? dateTime = null, int? statDays = null)
    {
        // количество заработанных топ-менеджментом за сегодня денег
        // + статистика по дням
        
        var result = await getManagementBalanceCommand.GetBalanceAsync(accountId, dateTime, statDays);
        
        return AccountBalanceMapping.MapFromDomainResult(result);
    }
}