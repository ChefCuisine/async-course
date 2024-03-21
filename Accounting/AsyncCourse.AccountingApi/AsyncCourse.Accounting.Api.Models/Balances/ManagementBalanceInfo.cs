namespace AsyncCourse.Accounting.Api.Models.Balances;

public class ManagementBalanceInfo
{
    public decimal? Total { get; set; }
    public List<ManagementBalanceDayInfo> Days { get; set; } = new();
}

public class ManagementBalanceDayInfo
{
    public DateTime Date { get; set; }
    public decimal? Total { get; set; }
}