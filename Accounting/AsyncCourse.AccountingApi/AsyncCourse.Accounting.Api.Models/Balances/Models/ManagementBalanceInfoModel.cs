namespace AsyncCourse.Accounting.Api.Models.Balances.Models;

public class ManagementBalanceInfoModel
{
    public decimal? Total { get; set; }
    public List<ManagementBalanceDayInfoModel> Days { get; set; } = new();
}

public class ManagementBalanceDayInfoModel
{
    public DateTime Date { get; set; }
    public decimal? Total { get; set; }
}