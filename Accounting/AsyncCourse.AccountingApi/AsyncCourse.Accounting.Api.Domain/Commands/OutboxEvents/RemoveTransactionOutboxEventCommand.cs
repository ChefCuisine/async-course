using AsyncCourse.Accounting.Api.Domain.Repositories.OutboxEvents;

namespace AsyncCourse.Accounting.Api.Domain.Commands.OutboxEvents;

public interface IRemoveTransactionOutboxEventCommand
{
    Task Remove(Guid id);
}

public class RemoveTransactionOutboxEventCommand : IRemoveTransactionOutboxEventCommand
{
    private readonly ITransactionOutboxEventRepository transactionOutboxEventRepository;

    public RemoveTransactionOutboxEventCommand(ITransactionOutboxEventRepository transactionOutboxEventRepository)
    {
        this.transactionOutboxEventRepository = transactionOutboxEventRepository;
    }

    public async Task Remove(Guid id)
    {
        await transactionOutboxEventRepository.DeleteAsync(id);
    }
}