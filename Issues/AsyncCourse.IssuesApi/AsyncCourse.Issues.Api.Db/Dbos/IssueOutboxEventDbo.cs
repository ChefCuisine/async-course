using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Db.Dbos;

[Table("issue-events")]
public class IssueOutboxEventDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("created_at", TypeName = "date")]
    public DateTime CreatedAt { get; set; }
    
    [Column("type")]
    public IssueOutboxEventType Type { get; set; }
    
    [Column("issue_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid IssueId { get; set; }
    
    [Column("title")]
    public string Title { get; set; }
    
    [Column("description")]
    public string Description { get; set; }

    [Column("status")]
    public string IssueStatus { get; set; }
    
    [Column("assigned_to_accound_id")]
    public Guid? AssignedToAccountId { get; set; }
}