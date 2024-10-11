using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.Model;

[Table("credentials")]
[PrimaryKey(nameof(ProjectId), nameof(Principal))]
public class Credentials
{
    [Column("project_id")]
    public string ProjectId { get; set; }
    [Column("principal")]
    public string Principal { get; set; }
    [Column("secret_hash")]
    public string SecretHash { get; set; }
}