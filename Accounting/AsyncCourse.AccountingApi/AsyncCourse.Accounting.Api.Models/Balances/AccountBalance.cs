namespace AsyncCourse.Accounting.Api.Models.Balances;

public class AccountBalance
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateTime Date { get; set; }
    public decimal? Total { get; set; }
}