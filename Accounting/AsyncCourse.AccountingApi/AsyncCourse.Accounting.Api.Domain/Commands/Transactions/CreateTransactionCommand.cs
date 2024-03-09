using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Transactions;

public interface ICreateTransactionCommand
{
    
}

public class CreateTransactionCommand : ICreateTransactionCommand
{
    private readonly ITransactionRepository transactionRepository;

    public CreateTransactionCommand(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }
}