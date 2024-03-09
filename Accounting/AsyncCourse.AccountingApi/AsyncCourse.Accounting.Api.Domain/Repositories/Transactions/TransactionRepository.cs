using AsyncCourse.Accounting.Api.Db;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;

public class ITransactionRepository
{
    
}

public class TransactionRepository : ITransactionRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;

    public TransactionRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }
}