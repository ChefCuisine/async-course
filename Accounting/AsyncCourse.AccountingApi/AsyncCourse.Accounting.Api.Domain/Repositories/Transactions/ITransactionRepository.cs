using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);

    Task<Transaction> GetAsync(Guid id);
}