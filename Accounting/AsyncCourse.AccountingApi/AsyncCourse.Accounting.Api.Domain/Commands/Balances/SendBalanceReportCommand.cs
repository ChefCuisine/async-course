using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface ISendBalanceReportCommand
{
    Task SendAsync(Guid transactionId);
}

public class SendBalanceReportCommand : ISendBalanceReportCommand
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IAccountRepository accountRepository;

    public SendBalanceReportCommand(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository)
    {
        this.transactionRepository = transactionRepository;
        this.accountRepository = accountRepository;
    }

    public async Task SendAsync(Guid transactionId)
    {
        var transaction = await transactionRepository.GetAsync(transactionId);

        if (transaction.Type != TransactionType.DayClosed)
        {
            return;
        }

        var closedDayTransactionInfo = transaction.ClosedDayInfo;

        var account = await accountRepository.GetAsync(transaction.ClosedDayInfo.AccountId);
        var email = account.Email;
        
        // Отправляем письмо на указанный account.Email с описанием closedDayTransactionInfo

        // todo написать отправку на почту
    }
}