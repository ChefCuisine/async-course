using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface IUpdateBalanceCommand
{
    Task UpdateBalanceAsync(Guid transactionId);
}

public class UpdateBalanceCommand : IUpdateBalanceCommand
{
    private readonly IAccountBalanceRepository accountBalanceRepository;
    private readonly ITransactionRepository transactionRepository;

    public UpdateBalanceCommand(
        IAccountBalanceRepository accountBalanceRepository,
        ITransactionRepository transactionRepository)
    {
        this.accountBalanceRepository = accountBalanceRepository;
        this.transactionRepository = transactionRepository;
    }

    public async Task UpdateBalanceAsync(Guid transactionId)
    {
        var transaction = await transactionRepository.GetAsync(transactionId);
        if (transaction?.IssueInfo == null || !transaction.Amount.HasValue)
        {
            return;
        }

        var accountId = transaction.IssueInfo.AssignToAccountId;

        var amountToAdd = transaction.Type == TransactionType.IssueAssigned
            ? -transaction.Amount.Value
            : transaction.Amount.Value;

        await accountBalanceRepository.UpdateAsync(accountId, transaction.CreatedAt, amountToAdd);
    }
}