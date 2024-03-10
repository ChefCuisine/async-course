using AsyncCourse.Accounting.Api.Models.OutboxEvents;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.OutboxEvents;

public interface ITransactionOutboxEventRepository
{
    Task AddAsync(TransactionOutboxEvent transactionEvent);
    Task AddBatchAsync(IEnumerable<TransactionOutboxEvent> issues);
    Task<TransactionOutboxEvent> GetAsync(Guid id);
    Task<TransactionOutboxEvent> GetNextAsync();
    Task DeleteAsync(Guid id);
}