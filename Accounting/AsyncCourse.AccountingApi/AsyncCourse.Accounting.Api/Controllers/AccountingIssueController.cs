using AsyncCourse.Accounting.Api.Domain.Commands.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("accounting-issue")]
public class AccountingIssueController : Controller
{
    private readonly IAddIssueCommand addIssueCommand;
    private readonly IReassignIssueCommand reassignIssueCommand;
    private readonly ICloseIssueCommand closeIssueCommand;

    public AccountingIssueController(
        IAddIssueCommand addIssueCommand,
        IReassignIssueCommand reassignIssueCommand,
        ICloseIssueCommand closeIssueCommand)
    {
        this.addIssueCommand = addIssueCommand;
        this.reassignIssueCommand = reassignIssueCommand;
        this.closeIssueCommand = closeIssueCommand;
    }
    
    [HttpPost("save")]
    public async Task<bool> Save(AccountingIssue issue)
    {
        await addIssueCommand.AddAsync(issue);

        return true;
    }
    
    [HttpPost("reassign")]
    public async Task<bool> Reassign(AccountingBusinessChangedIssue changedIssue)
    {
        await reassignIssueCommand.ReassignAsync(changedIssue);

        return true;
    }
    
    [HttpPost("close")]
    public async Task<bool> Close(AccountingBusinessChangedIssue changedIssue)
    {
        await closeIssueCommand.CloseAsync(changedIssue);

        return true;
    }
}