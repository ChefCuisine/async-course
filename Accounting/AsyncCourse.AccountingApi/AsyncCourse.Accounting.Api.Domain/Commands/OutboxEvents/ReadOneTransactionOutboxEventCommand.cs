using AsyncCourse.Accounting.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;

namespace AsyncCourse.Accounting.Api.Domain.Commands.OutboxEvents;

public interface IReadOneTransactionOutboxEventCommand
{
    Task<TransactionOutboxEvent> Read();
}

public class ReadOneTransactionOutboxEventCommand : IReadOneTransactionOutboxEventCommand
{
    private readonly ITransactionOutboxEventRepository transactionOutboxEventRepository;

    public ReadOneTransactionOutboxEventCommand(ITransactionOutboxEventRepository transactionOutboxEventRepository)
    {
        this.transactionOutboxEventRepository = transactionOutboxEventRepository;
    }
    
    public async Task<TransactionOutboxEvent> Read()
    {
        return await transactionOutboxEventRepository.GetNextAsync();
    }
}