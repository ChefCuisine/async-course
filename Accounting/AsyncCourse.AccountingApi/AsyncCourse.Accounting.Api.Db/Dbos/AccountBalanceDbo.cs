using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncCourse.Accounting.Api.Db.Dbos;

[Table("account-balances")]
public class AccountBalanceDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("account_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid AccountId { get; set; }
    
    [Column("date", TypeName = "date")]
    public DateTime Date { get; set; }
    
    [Column("total")]
    public decimal? Total { get; set; }
}