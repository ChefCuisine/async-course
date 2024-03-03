using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Db.Dbos;

[Table("issues")]
public class IssueDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Column("title")]
    public string Title { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("status")]
    public IssueStatus Status { get; set; }
    
    [Column("accound_id")]
    public Guid? AccountId { get; set; }
}