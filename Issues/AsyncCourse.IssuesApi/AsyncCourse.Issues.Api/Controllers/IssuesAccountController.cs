using AsyncCourse.Issues.Api.Domain.Commands.IssuesAccounts;
using AsyncCourse.Issues.Api.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.Issues.Api.Controllers;

[Route("issues-account")]
public class IssuesAccountController : Controller
{
    private readonly IAddIssueAccountCommand addAccountCommand;
    private readonly IUpdateIssueAccountCommand updateIssueAccountCommand;

    public IssuesAccountController(
        IAddIssueAccountCommand addAccountCommand,
        IUpdateIssueAccountCommand updateIssueAccountCommand)
    {
        this.addAccountCommand = addAccountCommand;
        this.updateIssueAccountCommand = updateIssueAccountCommand;
    }

    [HttpPost("save")]
    public async Task<bool> Save(IssueAccount account)
    {
        await addAccountCommand.AddAsync(account);

        return true;
    }
    
    [HttpPost("update")]
    public async Task<bool> Update(IssueAccount account)
    {
        await updateIssueAccountCommand.UpdateAsync(account);

        return true;
    }
}