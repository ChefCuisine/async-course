using AsyncCourse.Accounting.Api.Domain.Commands.Balances;
using AsyncCourse.Accounting.Api.Models.Balances;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("accounting-balance")]
public class AccountingBalanceController : Controller
{
    private readonly IUpdateBalanceCommand updateBalanceCommand;
    private readonly IGetAllBalanceCommand getAllBalanceCommand;
    private readonly IRenewBalanceCommand renewBalanceCommand;
    private readonly ICreateDayResultCommand createDayResultCommand;
    private readonly ISendBalanceReportCommand sendBalanceReportCommand;

    public AccountingBalanceController(
        IUpdateBalanceCommand updateBalanceCommand,
        IGetAllBalanceCommand getAllBalanceCommand,
        IRenewBalanceCommand renewBalanceCommand,
        ICreateDayResultCommand createDayResultCommand,
        ISendBalanceReportCommand sendBalanceReportCommand)
    {
        this.updateBalanceCommand = updateBalanceCommand;
        this.getAllBalanceCommand = getAllBalanceCommand;
        this.renewBalanceCommand = renewBalanceCommand;
        this.createDayResultCommand = createDayResultCommand;
        this.sendBalanceReportCommand = sendBalanceReportCommand;
    }
    
    [HttpPost("update")]
    public async Task<bool> Update([FromQuery] Guid transactionId)
    {
        await updateBalanceCommand.UpdateBalanceAsync(transactionId);

        return true;
    }
    
    [HttpGet("all-for-date")]
    public async Task<List<AccountBalance>> GetAllForDate([FromQuery] DateTime? dateTime = null)
    {
        var result = await getAllBalanceCommand.GetAsync(dateTime);

        return result;
    }
    
    [HttpPost("renew")]
    public async Task<bool> Renew([FromQuery] Guid accountId, [FromQuery] DateTime dateTime)
    {
        await renewBalanceCommand.RenewBalanceAsync(accountId, dateTime);

        return true;
    }
    
    [HttpPost("create-day-result")]
    public async Task<bool> CreateDayResult([FromQuery] Guid accountId, [FromQuery] DateTime dateTime)
    {
        await createDayResultCommand.CreateAsync(accountId, dateTime);

        return true;
    }
    
    [HttpPost("send-report")]
    public async Task<bool> SendEmail([FromQuery] Guid transactionId)
    {
        await sendBalanceReportCommand.SendAsync(transactionId);

        return true;
    }
}