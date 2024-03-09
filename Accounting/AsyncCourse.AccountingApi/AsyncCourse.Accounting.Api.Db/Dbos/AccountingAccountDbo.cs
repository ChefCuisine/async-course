using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Accounting.Api.Models.Accounts;

namespace AsyncCourse.Accounting.Api.Db.Dbos;

[Table("accounts")]
public class AccountingAccountDbo
{
    [Column("account_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid AccountId { get; set; }
    
    [Column("email")]
    public string Email { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("surname")]
    public string Surname { get; set; }

    [Column("role")]
    public AccountingAccountRole Role { get; set; }
}