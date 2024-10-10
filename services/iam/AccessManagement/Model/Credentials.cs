using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManagement.Model;

[Table("credentials")]
public class Credentials
{
    [Column("credentials_id")]
    public string Id { get; set; }
    [Column("principal_id")]
    public string PrincipalId { get; set; }
    [Column("secret")]
    public string Secret { get; set; }
}