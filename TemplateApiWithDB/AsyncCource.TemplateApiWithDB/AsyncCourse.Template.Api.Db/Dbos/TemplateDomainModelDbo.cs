using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncCourse.Template.Api.Db.Dbos;

[Table("template_domain_models")]
public class TemplateDomainModelDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("surname")]
    public string Surname { get; set; }
}