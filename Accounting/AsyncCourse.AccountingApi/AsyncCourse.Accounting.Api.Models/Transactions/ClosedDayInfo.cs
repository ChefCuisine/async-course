using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Models.Transactions;

public class ClosedDayInfo
{
    public Guid AccountId { get; set; }
    public List<TransactionBalanceInfo> Transactions { get; set; }
}