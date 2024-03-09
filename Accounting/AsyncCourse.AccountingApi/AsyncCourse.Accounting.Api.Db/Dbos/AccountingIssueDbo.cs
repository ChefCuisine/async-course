using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Accounting.Api.Models.Issues;

namespace AsyncCourse.Accounting.Api.Db.Dbos;

[Table("issues")]
public class AccountingIssueDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid IssueId { get; set; }

    [Column("title")]
    public string Title { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("status")]
    public AccountingIssueStatus Status { get; set; }
    
    [Column("assigned_to_accound_id")]
    public Guid? AssignedToAccountId { get; set; }
}