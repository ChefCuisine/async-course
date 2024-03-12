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
    
    [Column("jira_id")]
    public string JiraId { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("status")]
    public IssueStatus Status { get; set; }
    
    [Column("assigned_to_accound_id")]
    public Guid? AssignedToAccountId { get; set; }
}