using AsyncCourse.Issues.Api.Domain.Commands.OutboxEvents;
using AsyncCourse.Issues.Api.Models.OutboxEvents;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.Issues.Api.Controllers;

[Route("issues-event")]
public class IssueOutboxEventController : Controller
{
    private readonly IReadOneIssueOutboxEventCommand readOneIssueOutboxEventCommand;
    private readonly IRemoveIssueOutboxEventCommand removeIssueOutboxEventCommand;
    
    public IssueOutboxEventController(
        IReadOneIssueOutboxEventCommand readOneIssueOutboxEventCommand,
        IRemoveIssueOutboxEventCommand removeIssueOutboxEventCommand)
    {
        this.readOneIssueOutboxEventCommand = readOneIssueOutboxEventCommand;
        this.removeIssueOutboxEventCommand = removeIssueOutboxEventCommand;
    }
    
    [HttpGet("read-one")]
    public async Task<IssueOutboxEvent> ReadOne()
    {
        await readOneIssueOutboxEventCommand.Read();

        return await readOneIssueOutboxEventCommand.Read();;
    }
    
    [HttpDelete("delete")]
    public async Task<bool> Delete([FromRoute] Guid id)
    {
        await removeIssueOutboxEventCommand.Remove(id);

        return true;
    }
}