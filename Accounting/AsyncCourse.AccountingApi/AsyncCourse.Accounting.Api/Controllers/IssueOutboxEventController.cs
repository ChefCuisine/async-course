using AsyncCourse.Accounting.Api.Domain.Commands.OutboxEvents;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("transactions-event")]
public class IssueOutboxEventController : Controller
{
    private readonly IReadOneTransactionOutboxEventCommand readOneTransactionOutboxEventCommand;
    private readonly IRemoveTransactionOutboxEventCommand removeTransactionOutboxEventCommand;
    
    public IssueOutboxEventController(
        IReadOneTransactionOutboxEventCommand readOneTransactionOutboxEventCommand,
        IRemoveTransactionOutboxEventCommand removeTransactionOutboxEventCommand)
    {
        this.readOneTransactionOutboxEventCommand = readOneTransactionOutboxEventCommand;
        this.removeTransactionOutboxEventCommand = removeTransactionOutboxEventCommand;
    }
    
    [HttpGet("read-one")]
    public async Task<TransactionOutboxEvent> ReadOne()
    {
        return await readOneTransactionOutboxEventCommand.Read();
    }
    
    [HttpDelete("delete")]
    public async Task<bool> Delete([FromQuery] Guid id)
    {
        await removeTransactionOutboxEventCommand.Remove(id);

        return true;
    }
}