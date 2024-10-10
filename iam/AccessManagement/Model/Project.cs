using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManagement.Model;

[Table("project")]
public class Project
{
    [Column("project_id")]
    public string ProjectId { get; set; }
    [Column("user_id")]
    public string UserId { get; set; }
    [Column("name")]
    [MaxLength(200)]
    public string Name { get; set; }
    public List<Credentials> Credentials { get; set; }
    [NotMapped]
    public List<Policy.Policy> Policies { get; set; }
}