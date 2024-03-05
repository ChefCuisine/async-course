using AsyncCourse.Issues.Api.Domain.Commands.IssuesAccounts;
using AsyncCourse.Issues.Api.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.Issues.Api.Controllers;

[Route("issues-account")]
public class IssuesAccountController : Controller
{
    private readonly IAddIssueAccountCommand addAccountCommand;

    public IssuesAccountController(IAddIssueAccountCommand addAccountCommand)
    {
        this.addAccountCommand = addAccountCommand;
    }

    [HttpPost("save")]
    public async Task<bool> Save(IssueAccount account)
    {
        await addAccountCommand.AddAsync(account);

        return true;
    }
}