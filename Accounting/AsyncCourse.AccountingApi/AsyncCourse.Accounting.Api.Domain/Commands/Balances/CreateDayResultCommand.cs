using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Creator;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface ICreateDayResultCommand
{
    Task CreateAsync(Guid accountId, DateTime dateTime);
}

public class CreateDayResultCommand : ICreateDayResultCommand
{
    private readonly ITransactionsCreator transactionsCreator;

    public CreateDayResultCommand(ITransactionsCreator transactionsCreator)
    {
        this.transactionsCreator = transactionsCreator;
    }

    public async Task CreateAsync(Guid accountId, DateTime dateTime)
    {
        await transactionsCreator.CreateDayResultAsync(accountId, dateTime);
    }
}