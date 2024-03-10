using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.OutboxEvents;

public class TransactionOutboxEventRepository : ITransactionOutboxEventRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;

    public TransactionOutboxEventRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task AddAsync(TransactionOutboxEvent transactionEvent)
    {
        await accountingApiDbContext.TransactionEvents.AddAsync(DomainToDbo(transactionEvent));

        await accountingApiDbContext.SaveChangesAsync();
    }
    
    public async Task AddBatchAsync(IEnumerable<TransactionOutboxEvent> transactionEvents)
    {
        var mappedDbos = transactionEvents.Select(DomainToDbo);
        await accountingApiDbContext.TransactionEvents.AddRangeAsync(mappedDbos);

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task<TransactionOutboxEvent> GetAsync(Guid id)
    {
        var dbo = await accountingApiDbContext.TransactionEvents.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task<TransactionOutboxEvent> GetNextAsync()
    {
        var dbo = accountingApiDbContext.TransactionEvents
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault();
        
        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbo = await accountingApiDbContext.TransactionEvents.FindAsync(id);

        if (dbo != null)
        {
            return;
        }

        accountingApiDbContext.TransactionEvents.Remove(dbo);
        await accountingApiDbContext.SaveChangesAsync();
    }
    
    #region Mapping

    private static TransactionOutboxEventDbo DomainToDbo(TransactionOutboxEvent transactionEvent)
    {
        return new TransactionOutboxEventDbo
        {
            Id = transactionEvent.Id == Guid.Empty ? Guid.NewGuid() : transactionEvent.Id,
            CreatedAt = transactionEvent.CreatedAt,
            Type = transactionEvent.Type,
            TransactionId = transactionEvent.TransactionId,
            // todo
        };
    }

    private static TransactionOutboxEvent DboToDomain(TransactionOutboxEventDbo dbo)
    {
        return new TransactionOutboxEvent
        {
            Id = dbo.Id,
            CreatedAt = dbo.CreatedAt,
            Type = dbo.Type,
            TransactionId = dbo.TransactionId, 
            // todo
        };
    }

    #endregion
}