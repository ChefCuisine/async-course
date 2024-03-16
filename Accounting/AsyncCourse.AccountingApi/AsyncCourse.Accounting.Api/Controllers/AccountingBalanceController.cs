using AsyncCourse.Accounting.Api.Domain.Commands.Balances;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("accounting-balance")]
public class AccountingBalanceController : Controller
{
    private readonly IUpdateBalanceCommand updateBalanceCommand;

    public AccountingBalanceController(IUpdateBalanceCommand updateBalanceCommand)
    {
        this.updateBalanceCommand = updateBalanceCommand;
    }
    
    [HttpPost("update")]
    public async Task<bool> Update([FromQuery] Guid transactionId)
    {
        await updateBalanceCommand.UpdateBalanceAsync(transactionId);

        return true;
    }
}