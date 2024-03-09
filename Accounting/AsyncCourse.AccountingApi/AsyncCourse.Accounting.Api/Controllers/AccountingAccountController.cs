using AsyncCourse.Accounting.Api.Domain.Commands.Accounts;
using AsyncCourse.Accounting.Api.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("accounting-account")]
public class AccountingAccountController : Controller
{
    private readonly IAddAccountCommand addAccountCommand;
    private readonly IUpdateAccountCommand updateAccountCommand;

    public AccountingAccountController(
        IAddAccountCommand addAccountCommand,
        IUpdateAccountCommand updateAccountCommand)
    {
        this.addAccountCommand = addAccountCommand;
        this.updateAccountCommand = updateAccountCommand;
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
}