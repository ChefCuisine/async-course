﻿using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;

public class TransactionRepository : ITransactionRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;

    public TransactionRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task AddAsync(Transaction transaction)
    {
        await accountingApiDbContext.Transactions.AddAsync(DomainToDbo(transaction));

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task<Transaction> GetAsync(Guid id)
    {
        var dbo = await accountingApiDbContext.Transactions.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    #region Mapping

    private static TransactionDbo DomainToDbo(Transaction transaction)
    {
        return new TransactionDbo
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
            Type = transaction.Type,
            IssueInfo = transaction.IssueInfo,
            Amount = transaction.Amount
        };
    }

    private static Transaction DboToDomain(TransactionDbo dbo)
    {
        return new Transaction
        {
            Id = dbo.Id,
            CreatedAt = dbo.CreatedAt,
            Type = dbo.Type,
            IssueInfo = dbo.IssueInfo,
            Amount = dbo.Amount
        };
    }
    
    #endregion
}